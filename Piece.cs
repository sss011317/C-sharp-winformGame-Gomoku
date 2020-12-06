using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;


namespace Gomoku
{
    /// <summary>
    /// protect instance the piece to be class, so be a abstract
    /// </summary>
    abstract class Piece : PictureBox
    {
        private static readonly int IMAGE_WIDTH = 50;
        private static readonly int IMAGE_HIGH = 50;
        public Piece(int x , int y) 
        {
            this.BackColor = Color.Transparent;
            this.Location = new Point(x - IMAGE_HIGH/2, y - IMAGE_WIDTH/2);
            this.Size = new Size(IMAGE_WIDTH, IMAGE_HIGH);
        }
        public abstract PieceType GetPieceType();

    }
}