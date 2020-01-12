using Chess.Old;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chess.DesktopUI
{
    public partial class Form1 : Form
    {
        private int _boardWidth;
        private Point _boardCorner;
        private Bitmap _boardImage;
        private Point _SelectedCell = new Point(-1, -1);
        private State _state;

        public Form1()
        {
            InitializeComponent();
            DoubleBuffered = true;

            _state = StateFactory.GetPawnStartingState(8, 8);

        }



        private Bitmap GetBoardBitmap(int width, int height)
        {
            var squareWidth = width / 8;

            Bitmap bm = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(bm))
            using (SolidBrush blackBrush = new SolidBrush(Color.Black))
            using (SolidBrush whiteBrush = new SolidBrush(Color.White))
            using (SolidBrush redBrush = new SolidBrush(Color.Red))
            using (Font pieceFont = new Font("Arial", 32))
            using (SolidBrush blackPieceBrush = new SolidBrush(Color.DarkGray))
            using (SolidBrush whitePieceBrush = new SolidBrush(Color.LightGray))
            {
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        //draw squares
                        if (j == _SelectedCell.X && i == _SelectedCell.Y)
                        {
                            g.FillRectangle(redBrush, i * squareWidth, j * squareWidth, squareWidth, squareWidth);
                        }
                        else if ((j % 2 == 0 && i % 2 == 0) || (j % 2 != 0 && i % 2 != 0))
                            g.FillRectangle(blackBrush, i * squareWidth, j * squareWidth, squareWidth, squareWidth);
                        else if ((j % 2 == 0 && i % 2 != 0) || (j % 2 != 0 && i % 2 == 0))
                            g.FillRectangle(whiteBrush, i * squareWidth, j * squareWidth, squareWidth, squareWidth);

                        //draw piece
                        int ci = i * squareWidth; //+ squareWidth / 2;
                        int cj = j * squareWidth; //+ squareWidth / 2;
                        if (_state.Board[i, j].Player == EnumPlayer.White)
                        {
                            switch (_state.Board[i, j].Piece)
                            {
                                case EnumPiece.Pawn:
                                    g.DrawString("P", pieceFont, whitePieceBrush, cj, ci);
                                    break;
                            }
                        }
                        else if (_state.Board[i, j].Player == EnumPlayer.Black)
                        {
                            switch (_state.Board[i, j].Piece)
                            {
                                case EnumPiece.Pawn:
                                    g.DrawString("P", pieceFont, blackPieceBrush, cj, ci);
                                    break;
                            }
                        }
                    }
                }
                return bm;
            }
        }

        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            RecalculateBoardImage();
            this.Refresh();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //if(_boardImage == null)
            //{
                RecalculateBoardImage();
            //}

            int x = (this.ClientSize.Width - _boardWidth) / 2;
            int y = (this.ClientSize.Height - _boardWidth) / 2;

            _boardCorner = new Point(x, y);

            e.Graphics.DrawImage(_boardImage, x, y);
        }

        private void RecalculateBoardImage()
        {
            int boarderWidth = 10;
            _boardWidth = Math.Min(this.ClientSize.Width, this.ClientSize.Height) - 2 * boarderWidth;

            if (_boardImage != null)
            {
                _boardImage.Dispose();
            }

            _boardImage = GetBoardBitmap(_boardWidth, _boardWidth);
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                
                    var row = (8 * (e.Y - _boardCorner.Y)) / _boardWidth;
                    var col = (8 * (e.X - _boardCorner.X)) / _boardWidth;

                    _SelectedCell = new Point(row, col);
               
            }
            if (e.Button == MouseButtons.Right)
            {
                _SelectedCell = new Point(-1, -1);

            }
            RecalculateBoardImage();
            this.Refresh();
        }
    }
}
