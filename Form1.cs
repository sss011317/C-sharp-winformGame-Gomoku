using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gomoku
{

    public partial class Form1 : Form
    {
        private void Restart()
        {
            System.Threading.Thread thtmp = new System.Threading.Thread(new
            System.Threading.ParameterizedThreadStart(run));

            object appName = Application.ExecutablePath;
            System.Threading.Thread.Sleep(100);
            thtmp.Start(appName);
        }

        private void run(Object obj)
        {
            System.Diagnostics.Process ps = new System.Diagnostics.Process();
            ps.StartInfo.FileName = obj.ToString();
            ps.Start();
        }

        private Game game = new Game();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            Piece piece = game.PlaceAPiece(e.X, e.Y);
           
            
            if(piece != null)
            {
                this.Controls.Add(piece);
                
                //check who is winner
                if (game.Winner == PieceType.BLACK) 
                {
                    MessageBox.Show("Black player win");
                    
                    Application.ExitThread();
                    Restart();
                }
                if (game.Winner == PieceType.WHITE)
                {
                    MessageBox.Show("White player win");
                        
                    Application.ExitThread();
                    Restart();
                }
            }

        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (game.CanBePlaced(e.X, e.Y))
            {
                this.Cursor = Cursors.Hand;
            }
            else
            {
                this.Cursor = Cursors.Default;
            }
        }
    }
}
