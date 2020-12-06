using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gomoku
{
    class Game
    {
        private Board board = new Board();

        private PieceType currentPlayer = PieceType.BLACK;

        private PieceType winner = PieceType.NONE;
        public PieceType Winner { get { return winner; } }
        public bool CanBePlaced(int x, int y)
        {
            return board.CanBePlaced(x, y);
        }
        
        public Piece PlaceAPiece(int x, int y)
        {
            Piece piece = board.PlaceAPiece(x, y, currentPlayer);
            if (piece != null)
            {
                //check the Winner
                checkWinner();
                //switch player
                if (currentPlayer == PieceType.BLACK)
                    currentPlayer = PieceType.WHITE;
                else if (currentPlayer == PieceType.WHITE)
                    currentPlayer = PieceType.BLACK;

                return piece;
            }

            return null;
        }
        private void checkWinner()
        {
            int centerX = board.LastPlaceNode.X;
            int centerY = board.LastPlaceNode.Y;
            /***
            check the eight direction
            --------------------------------
            |  (-1,-1) | (+0,-1) | (+1,-1) |
            |------------------------------|
            |  (-1,0)  | (+0,+0) | (+1,0)  |
            |------------------------------|
            |  (-1,+1) | (+0,+1) | (+1,+1) |
            --------------------------------
            ***/
            for (int xDir = -1; xDir <= 1; xDir++)
            {
                for(int yDir =-1; yDir <=1; yDir++)
                {
                    //jump the middle one(not the direction to middle)
                    if (xDir == 0 & yDir == 0)
                        continue;
                    //record the count 
                    int winnerCount = 1;
                    //if search the piece is not the same color , return False
                    bool target = true;
                    bool oppoisit = true;

                    for (int count=1;count<=5;count++)
                    {
                        int targetX = centerX + count * xDir;
                        int targetY = centerY + count * yDir;
                        int oppoistetargetX = centerX + count * xDir * -1;
                        int oppoistetargetY = centerY + count * yDir * -1;

                        //check the front side is right or not
                        if (target)
                        {
                            if (targetX < 0 || targetX >= Board.NODE_COUNT ||
                                targetY < 0 || targetY >= Board.NODE_COUNT ||
                                board.GetPieceType(targetX, targetY) != currentPlayer)
                            {
                                target = false;
                            }
                            else
                                winnerCount++;
                        }
                        //check the backside is right or not
                        if (oppoisit)
                        {
                            if (oppoistetargetX < 0 || oppoistetargetX >= Board.NODE_COUNT ||
                                oppoistetargetY < 0 || oppoistetargetY >= Board.NODE_COUNT ||
                                board.GetPieceType(oppoistetargetX, oppoistetargetY) != currentPlayer)
                            {
                                oppoisit = false;
                            }
                            else
                                winnerCount++;
                        }
                        if (target==false & oppoisit == false)
                        {
                            break;
                        }
                        //if the count is 5 the winner 
                        if (winnerCount == 5)
                            winner = currentPlayer;
                    }
                }
                }
        }
    }
}
