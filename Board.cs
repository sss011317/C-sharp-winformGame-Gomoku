using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Gomoku
{
    class Board
    {

        public static readonly int NODE_COUNT = 9;
        private static readonly Point NO_MATCH_NODE = new Point(-1, -1);
        private static readonly int OFFSET = 75;
        private static readonly int NODE_RADIUS = 10;
        private static readonly int NODE_DISTANCE = 75;
        private Point lastPlaceNode = NO_MATCH_NODE;
        public Point LastPlaceNode { get { return lastPlaceNode;  }  }

        public Piece[,] pieces = new Piece[9, 9];
        
        public PieceType GetPieceType(int nodeIdX , int nodeIdY)
        {
            Console.WriteLine("nodIdx:"+nodeIdX+"   nodeIdY:" + nodeIdY);
            Console.Read();
            if (pieces[nodeIdX, nodeIdY] == null)
                return PieceType.NONE;
            else
                return pieces[nodeIdX, nodeIdY].GetPieceType();    
        }
        
        public bool CanBePlaced(int x , int y)
        {
            Point nodeId = findTheCloseNode(x, y);

            if (nodeId == NO_MATCH_NODE)
                return false;

            if (pieces[nodeId.X, nodeId.Y] != null)
            {
                return false;
            }
            return true;

        }
        public Piece PlaceAPiece(int x , int y , PieceType type)
        {
            //find a nearly black line
            Point nodeId = findTheCloseNode(x, y);

            //if mouse not on the black line , return null
            if (nodeId == NO_MATCH_NODE)
                return null;
            // check the mouse poisiton has to on the black line
            if(pieces[nodeId.X,nodeId.Y] != null)
            {
                return null;
            }
            //accroding to type of piece
            Point formPos = convertToFormPosition(nodeId);

            if(type == PieceType.BLACK) 
                pieces[nodeId.X, nodeId.Y] = new BlackPiece(formPos.X, formPos.Y); 

            else if (type == PieceType.WHITE)
                pieces[nodeId.X, nodeId.Y] = new WhitePiece(formPos.X, formPos.Y);
            //record the last position
            lastPlaceNode = nodeId;

            return pieces[nodeId.X, nodeId.Y];
        }

        private Point convertToFormPosition(Point nodeId)
        {
            Point formPosition = new Point();
            formPosition.X = nodeId.X * NODE_DISTANCE + OFFSET;
            formPosition.Y = nodeId.Y * NODE_DISTANCE + OFFSET;
            return formPosition;
        } 
        private Point findTheCloseNode(int x , int y)
        {
            int nodeIdX = findTheCloseNode(x);
            if(nodeIdX == -1 || nodeIdX >= NODE_COUNT)
                return NO_MATCH_NODE;

            int nodeIdY = findTheCloseNode(y);
            if (nodeIdY == -1 || nodeIdY >= NODE_COUNT)
                return NO_MATCH_NODE;

            return new Point(nodeIdX, nodeIdY);
        }

        private int findTheCloseNode(int pos)
        {
            pos -= OFFSET;
            //find the Node of black line, 
            //qoutient 商數
            //reminader 餘數
            int qoutient = pos / NODE_DISTANCE;
            int reminader = pos % NODE_DISTANCE;

            if (reminader <= NODE_RADIUS)
                return qoutient;

            else if (reminader >= NODE_DISTANCE - NODE_RADIUS)
                return qoutient + 1;
            else
                return -1;

        }
    }
}
