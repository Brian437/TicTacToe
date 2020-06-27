using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NS_TicTacToe
{
    public partial class formTicTacToe : Form
    {
        /*
         * Brian Chaves
         * October 5 2012
         * Assinment 2
         * Tic Tac Toe Game
         */


        public formTicTacToe()
        {
            InitializeComponent();
        }

        const int int_SIZE = 200;
        const int int_MARGIN = 6;
        const int int_START_LOCATION = 12;
        const int int_NUMBER_OF_SQUARES_PER_SECTION = 3;
        const int int_BORDER_SIZE = 1;

        const int id_NO_WINNER = 0;
        const int id_X_WINS = 1;
        const int id_O_WINS = 2;
        const int id_DRAW = 3;

        PictureBox[,] picTicTacToe;

        char[,] chrSquare = new char[int_NUMBER_OF_SQUARES_PER_SECTION, int_NUMBER_OF_SQUARES_PER_SECTION];
        int idTurn = 0;

        int intX_Wins = 0;
        int intO_Wins = 0;
        int intDraws = 0;

        /// <summary>
        /// Form load Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void formTicTacToe_Load(object sender, EventArgs e)
        {

            FormIntro formIntro = new FormIntro();
            formIntro.ShowDialog();

            picTicTacToe = new PictureBox[int_NUMBER_OF_SQUARES_PER_SECTION, int_NUMBER_OF_SQUARES_PER_SECTION];

            for (int x = 0; x < int_NUMBER_OF_SQUARES_PER_SECTION; x++)
            {
                for (int y = 0; y < int_NUMBER_OF_SQUARES_PER_SECTION; y++)
                {
                    //Creates the picture box
                    picTicTacToe[x, y] = new PictureBox();
                    picTicTacToe[x, y].Top = int_START_LOCATION + (int_SIZE + int_MARGIN) * y;
                    picTicTacToe[x, y].Left = int_START_LOCATION + (int_SIZE + int_MARGIN) * x;
                    picTicTacToe[x, y].Width = int_SIZE;
                    picTicTacToe[x, y].Height = int_SIZE;
                    picTicTacToe[x, y].BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                    picTicTacToe[x, y].SizeMode = PictureBoxSizeMode.Zoom;
                    picTicTacToe[x, y].BackColor = Color.Black;
                    this.Controls.Add(picTicTacToe[x, y]);
                    chrSquare[x, y] = ' ';

                    picTicTacToe[x, y].Click += new EventHandler(picTicTacToe_Click);
                }
            }

            idTurn = 1;
            UpdateStatus();
        }
        /// <summary>
        /// Event that happen after clicking the image box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picTicTacToe_Click(object sender, EventArgs e)
        {
            int row = 0;
            int column = 0;
            int idWin = 0;

            for (int x = 0; x < int_NUMBER_OF_SQUARES_PER_SECTION; x++)
            {
                for (int y = 0; y < int_NUMBER_OF_SQUARES_PER_SECTION; y++)
                {
                    if (sender.Equals(picTicTacToe[x, y]))
                    {
                        column = x;
                        row = y;
                    }
                }
            }

            if (idTurn == 0)
            {
                //btnNewGame_Click(sender,e);
                NewGame();
                return;
            }

            if (chrSquare[column, row] != ' ')
            {
                MessageBox.Show("Select a different box", this.Text);
                return;
            }

            if (idTurn == 1)
            {
                picTicTacToe[column, row].Image = Properties.Resources.X;
                chrSquare[column, row] = 'X';
            }
            else if (idTurn == 2)
            {
                picTicTacToe[column, row].Image = Properties.Resources.O;
                chrSquare[column, row] = 'O';
            }

            idTurn = 3 - idTurn;
            idWin = CheckForWinner();
            EndGame(idWin);

            UpdateStatus();
            if (idWin != 0)
            {
                //btnNewGame_Click(sender, e);
                UpdateStatus();
            }

        }
        /// <summary>
        /// Updates Status
        /// </summary>
        private void UpdateStatus()
        {
            if (idTurn == 0)
            {
                btnNewGame.Visible = true;
                btnQuit.Text = "Exit Game";
                txtScore.Text = "End Of Game";
            }
            else
            {
                btnNewGame.Visible = false;
                btnQuit.Text = "Forfeit";

                if (idTurn == 1)
                {
                    txtScore.Text = "Turn: X";
                }
                else if (idTurn == 2)
                {
                    txtScore.Text = "Turn: O";
                }
            }

            if (intX_Wins > 0 || intO_Wins > 0 || intDraws > 0)
            {
                txtScore.Text += "\r\nWins for X: " + intX_Wins.ToString() +
                        "\r\nWins for O: " + intO_Wins.ToString();
                if (intDraws > 0)
                {
                    txtScore.Text += "\r\nDraws: " + intDraws.ToString();
                }
            }
        }
        /// <summary>
        /// Checks to see if there is a winner or a draw
        /// </summary>
        /// <returns></returns>
        private int CheckForWinner()
        {

            //Checks rows
            for (int x = 0; x < int_NUMBER_OF_SQUARES_PER_SECTION; x++)
            {
                for (int y = 0; y < int_NUMBER_OF_SQUARES_PER_SECTION; y++)
                {
                    if (chrSquare[x, y] == ' ')
                    {
                        break;
                    }

                    if (chrSquare[x, y] != chrSquare[x, 0])
                    {
                        break;
                    }

                    if (y == (int_NUMBER_OF_SQUARES_PER_SECTION - 1))
                    {
                        if (chrSquare[x, 0] == 'X')
                        {
                            return id_X_WINS;
                        }
                        else if (chrSquare[x, 0] == 'O')
                        {
                            return id_O_WINS;
                        }
                    }
                }
            }

            //Checks Columns
            for (int y = 0; y < int_NUMBER_OF_SQUARES_PER_SECTION; y++)
            {
                for (int x = 0; x < int_NUMBER_OF_SQUARES_PER_SECTION; x++)
                {
                    if (chrSquare[x, y] == ' ')
                    {
                        break;
                    }

                    if (chrSquare[x, y] != chrSquare[0, y])
                    {
                        break;
                    }

                    if (x == (int_NUMBER_OF_SQUARES_PER_SECTION - 1))
                    {
                        if (chrSquare[0, y] == 'X')
                        {
                            return id_X_WINS;
                        }
                        else if (chrSquare[0, y] == 'O')
                        {
                            return id_O_WINS;
                        }
                    }
                }
            }

            //Checks diagonal
            for (int x = 0; x < int_NUMBER_OF_SQUARES_PER_SECTION; x++)
            {
                if (chrSquare[x, x] == ' ')
                {
                    break;
                }
                if(chrSquare[x,x] != chrSquare[0,0])
                {
                    break;
                }

                if (x == (int_NUMBER_OF_SQUARES_PER_SECTION - 1))
                {
                    if (chrSquare[0, 0] == 'X')
                    {
                        return id_X_WINS;
                    }
                    else if (chrSquare[0, 0] == 'O')
                    {
                        return id_O_WINS;
                    }
                }
            }

            for (int x = 0; x < int_NUMBER_OF_SQUARES_PER_SECTION; x++)
            {
                int y = int_NUMBER_OF_SQUARES_PER_SECTION - x-1;

                if (chrSquare[x, y] == ' ')
                {
                    break;
                }
                if (chrSquare[x, y] != chrSquare[0, (int_NUMBER_OF_SQUARES_PER_SECTION-1)])
                {
                    break;
                }

                if (x == (int_NUMBER_OF_SQUARES_PER_SECTION - 1))
                {
                    if (chrSquare[0, (int_NUMBER_OF_SQUARES_PER_SECTION - 1)] == 'X')
                    {
                        return id_X_WINS;
                    }
                    else if (chrSquare[0, (int_NUMBER_OF_SQUARES_PER_SECTION - 1)] == 'O')
                    {
                        return id_O_WINS;
                    }
                }
            }


            //Checks draw
            bool fullSquare = true;
            for (int x = 0; x < int_NUMBER_OF_SQUARES_PER_SECTION; x++)
            {
                for (int y = 0; y < int_NUMBER_OF_SQUARES_PER_SECTION; y++)
                {
                    if (chrSquare[x,y] == ' ')
                    {
                        fullSquare = false;
                    }
                }
            }

            if (fullSquare)
            {
                return id_DRAW;
            }

            return id_NO_WINNER;
        }

        /// <summary>
        /// Clears all the boxes so a new game can be played again 
        /// </summary>
        private void ClearBoxes()
        {
            for (int x = 0; x < int_NUMBER_OF_SQUARES_PER_SECTION; x++)
            {
                for (int y = 0; y < int_NUMBER_OF_SQUARES_PER_SECTION; y++)
                {
                    picTicTacToe[x, y].Image = null;
                    chrSquare[x, y] = ' ';
                }
            }
        }

        /// <summary>
        /// Close the program or forfit game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuit_Click(object sender, EventArgs e)
        {
            if (idTurn == 0)
            {
                DialogResult result = MessageBox.Show("Exit Game", this.Text, MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    this.Close();
                }
            }
            else
            {
                DialogResult result = MessageBox.Show("Are you sure you want to forfeit?", this.Text, MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    if (idTurn == 1)
                    {
                        EndGame(2);
                    }
                    else if (idTurn == 2)
                    {
                        EndGame(1);
                    }
                    UpdateStatus();
                }
            }
        }
        /// <summary>
        /// Starts a new game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNewGame_Click(object sender, EventArgs e)
        {
            /*
            DialogResult result = MessageBox.Show("Start New Game?", this.Text, MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                idTurn = 1;
                ClearBoxes();
                UpdateStatus();
            }
             */
            NewGame();
        }

        private void NewGame(bool confirm=false)
        {
            DialogResult result = MessageBox.Show("Start New Game?", this.Text, MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                Random random = new Random();
                idTurn = random.Next(1,3);
                ClearBoxes();
                UpdateStatus();
            }
        }

        /// <summary>
        /// Ends the game
        /// </summary>
        /// <param name="idWin">ID of the winner</param>
        private void EndGame(int idWin)
        {
            if (idWin == id_X_WINS)
            {
                MessageBox.Show("X wins", this.Text);
                intX_Wins++;
                idTurn = 0;
            }
            else if (idWin == id_O_WINS)
            {
                MessageBox.Show("O wins", this.Text);
                intO_Wins++;
                idTurn = 0;
            }
            else if (idWin == id_DRAW)
            {
                MessageBox.Show("Draw", this.Text);
                intDraws++;
                idTurn = 0;
            }
        }
        /// <summary>
        /// Event on logo click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picLogo_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Created by Brian Chaves \r\nOctober 2012", this.Text);
        }
    }
}
