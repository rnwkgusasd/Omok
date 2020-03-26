using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Omok
{
    public partial class Form1 : Form
    {
        private const int ROW = 19;
        private const int COLUMN = 19;

        private const int DEFAULT_X = 14;
        private const int DEFAULT_Y = 14;

        private const int SIZE_WIDTH = 30;
        private const int SIZE_HEIGHT = 30;
        private const int SIZE_AREA = SIZE_WIDTH * SIZE_HEIGHT;

        private const int BUTTON_SPACE = 42;

        private Button[,] Board = new Button[ROW, COLUMN];

        private int Count = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            for(int tCol = 0; tCol < COLUMN; tCol++)
            {
                for(int tRow = 0; tRow < ROW; tRow++)
                {
                    Button btn = new Button
                    {
                        Location = new Point(DEFAULT_X + (BUTTON_SPACE * tCol), DEFAULT_Y + (BUTTON_SPACE * tRow)),
                        FlatStyle = FlatStyle.Flat,
                        Size = new Size(SIZE_WIDTH, SIZE_HEIGHT),
                    };
                    btn.BackColor = Color.Orange;
                    btn.FlatAppearance.BorderSize = 0;
                    btn.FlatAppearance.MouseOverBackColor = Color.YellowGreen;
                    btn.Click += (object s, EventArgs eA) =>
                    {
                        Button b = (Button)s;

                        if (b.BackColor == Color.Orange)
                        {
                            if (Count % 2 == 0) b.BackColor = Color.Black;
                            else b.BackColor = Color.White;

                            b.FlatAppearance.MouseOverBackColor = b.BackColor;
                        }

                        if(WinCheck())
                        {
                            MessageBox.Show(Count % 2 == 0 ? "BLACK WIN" : "WHITE WIN");
                            Application.Exit();
                        }

                        Count++;
                    };

                    transparentPanel1.Controls.Add(btn);
                    Board[tRow, tCol] = btn;
                }
            }
        }

        public bool WinCheck()
        {
            Color col = new Color();

            for(int i = 0; i < ROW; i++)
            {
                for(int j = 0; j < COLUMN; j++)
                {
                    if(Board[i, j].BackColor != Color.Orange)
                    {
                        if (NeighborCheck(j, i, Board[i, j].BackColor)) return true;
                    }
                }
            }

            return false;
        }

        public bool NeighborCheck(int ColIDX, int RowIDX, Color Colr)
        {
            if(ColIDX < 0 || ColIDX >= COLUMN || RowIDX < 0 || RowIDX >= ROW)
                return false;

            bool isOK = false;

            #region Columns Check
            // right
            for (int i = 1; i < 5; i++)
            {
                if (ColIDX + i >= COLUMN) { isOK = false; break; }

                if (Board[RowIDX, ColIDX].BackColor == Board[RowIDX, ColIDX + i].BackColor)
                {
                    if (i == 4) return true;
                    else continue;
                }
                else { isOK = false; break; }
            }

            // left
            for (int i = 1; i < 5; i++)
            {
                if (ColIDX - i < 0) { isOK = false; break; }

                if (Board[RowIDX, ColIDX].BackColor == Board[RowIDX, ColIDX - i].BackColor)
                {
                    if (i == 4) return true;
                    else continue;
                }
                else { isOK = false; break; }
            }
            #endregion

            #region Rows Check
            // down
            for (int i = 1; i < 5; i++)
            {
                if (RowIDX + i >= ROW) { isOK = false; break; }

                if (Board[RowIDX, ColIDX].BackColor == Board[RowIDX + i, ColIDX].BackColor)
                {
                    if (i == 4) return true;
                    else continue;
                }
                else { isOK = false; break; }
            }

            // up
            for (int i = 1; i < 5; i++)
            {
                if (RowIDX - i < 0) { isOK = false; break; }

                if (Board[RowIDX, ColIDX].BackColor == Board[RowIDX - i, ColIDX].BackColor)
                {
                    if (i == 4) return true;
                    else continue;
                }
                else { isOK = false; break; }
            }
            #endregion

            #region Side up Check
            // left up
            for (int i = 1; i < 5; i++)
            {
                if (ColIDX - i < 0 || RowIDX - i < 0) { isOK = false; break; }

                if (Board[RowIDX, ColIDX].BackColor == Board[RowIDX - i, ColIDX - i].BackColor)
                {
                    if (i == 4) return true;
                    else continue;
                }
                else { isOK = false; break; }
            }

            // left down
            for (int i = 1; i < 5; i++)
            {
                if (ColIDX - i < 0 || RowIDX + i >= ROW) { isOK = false; break; }

                if (Board[RowIDX, ColIDX].BackColor == Board[RowIDX + i, ColIDX - i].BackColor)
                {
                    if (i == 4) return true;
                    else continue;
                }
                else { isOK = false; break; }
            }

            // right up
            for (int i = 1; i < 5; i++)
            {
                if (ColIDX + i >= COLUMN || RowIDX - i < 0) { isOK = false; break; }

                if (Board[RowIDX, ColIDX].BackColor == Board[RowIDX - i, ColIDX + i].BackColor)
                {
                    if (i == 4) isOK = true;
                    else continue;
                }
                else { isOK = false; break; }
            }

            // right down
            for (int i = 1; i < 5; i++)
            {
                if (ColIDX + i >= COLUMN || RowIDX + i >= ROW) { isOK = false; break; }

                if (Board[RowIDX, ColIDX].BackColor == Board[RowIDX + i, ColIDX + i].BackColor)
                {
                    if (i == 4) return true;
                    else continue;
                }
                else { isOK = false; break; }
            }
            #endregion

            return isOK;
        }
    }

    public class TransparentPanel : Panel
    {
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x00000020; // WS_EX_TRANSPARENT
                return cp;
            }
        }
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            //base.OnPaintBackground(e);
        }
    }
}
