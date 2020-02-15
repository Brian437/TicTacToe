/* Brian Chaves
 * October 18
 * Assinment 3
 * 15 Squares
 * Game Form
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace bChaves_Assinment_3
{
    public partial class FormGame : Form
    {
        int intNumberOfRows = 0;
        int intNumberOfColumns = 0;
        int[,] intSquareValue;

        const int int_DEFALT_NUMBER_OF_COLUMNS = 4;
        const int int_DEFALT_NUMBER_OF_ROWS = 4;
        const int int_START_LEFT = 20;
        const int int_START_TOP = 40;
        const int int_LENGTH = 100;
        const int int_RIGHT_SPACE = 20;
        const int int_BOTTOM_SPACE = 40;
        const int int_FONT_SIZE = 30;
        const decimal dec_MOVE_COUNTER_TO_NUMBER_OF_SQUARE_RATIO = 25m;

        Label[,] lblSquare;

        /// <summary>
        /// Square click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblSquare_Click(object sender, EventArgs e)
        {
            int intColumn = 0;
            int intRow = 0;
            int intEmptyColumn;
            int intEmptyRow;
            
            for (int x = 0; x < intNumberOfColumns; x++)
            {
                for (int y = 0; y < intNumberOfRows; y++)
                {
                    if (sender.Equals(lblSquare[x, y]))
                    {
                        intColumn = x;
                        intRow = y;
                    }
                }
            }

            getEmptySquare(out intEmptyColumn, out intEmptyRow);

            if (intColumn == intEmptyColumn ^ intRow == intEmptyRow)
            {

                if (intRow > intEmptyRow)//Move Down
                {
                    for (int y = intEmptyRow; y < intRow; y++)
                    {
                        intSquareValue[intColumn, y] = intSquareValue[intColumn, y + 1];
                    }
                }
                else if (intRow < intEmptyRow)//Move Up
                {
                    for (int y = intEmptyRow; y > intRow; y--)
                    {
                        intSquareValue[intColumn, y] = intSquareValue[intColumn, y - 1];
                    }
                }
                else if (intColumn > intEmptyColumn)//Move Left
                {
                    for (int x = intEmptyColumn; x < intColumn; x++)
                    {
                        intSquareValue[x, intRow] = intSquareValue[x + 1, intRow];
                    }
                }
                else if (intColumn < intEmptyColumn)//Move Right
                {
                    for (int x = intEmptyColumn; x > intColumn; x--)
                    {
                        intSquareValue[x, intRow] = intSquareValue[x - 1, intRow];
                    }
                }
                intSquareValue[intColumn, intRow] = 0;
                updateGame();
            }

        }

        public FormGame()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Form load event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormGame_Load(object sender, EventArgs e)
        {
            CallIntroForm();
        }
        /// <summary>
        /// Gets the intro form
        /// </summary>
        private void CallIntroForm()
        {
            this.Hide();

            string strGameMode = "";
            FormIntro formIntro = new FormIntro();
            formIntro.ShowDialog();
            strGameMode = formIntro.StrGameMode;
            formIntro.Dispose();

            if (strGameMode == "original")
            {
                intNumberOfRows = int_DEFALT_NUMBER_OF_ROWS;
                intNumberOfColumns = int_DEFALT_NUMBER_OF_COLUMNS;
                createGame(true);
            }
            else if (strGameMode == "custom")
            {
                DlgCustom dlgCustom = new DlgCustom();
                dlgCustom.ShowDialog();
                intNumberOfColumns = dlgCustom.IntNumberOfColumns;
                intNumberOfRows = dlgCustom.IntNumberOfRows;
                dlgCustom.Dispose();

                if (intNumberOfColumns == -1)
                {
                    CallIntroForm();
                }
                else
                {
                    createGame(true);
                }
            }
            else if (strGameMode == "load game")
            {
                DialogResult result = openFileDialog.ShowDialog();
                switch (result)
                {
                    case DialogResult.Abort:
                        break;
                    case DialogResult.Cancel://Cancels load
                        CallIntroForm();
                        break;
                    case DialogResult.Ignore:
                        break;
                    case DialogResult.No:
                        break;
                    case DialogResult.None:
                        break;
                    case DialogResult.OK://finds load file
                        string strFileName = openFileDialog.FileName;
                        doLoad(strFileName);
                        break;
                    case DialogResult.Retry:
                        break;
                    case DialogResult.Yes:
                        break;
                    default:
                        break;
                }
            }
            else if (strGameMode == "quit")
            {
                this.Close();
            }
        }
        /// <summary>
        /// Loads the game
        /// </summary>
        /// <param name="strFileName">File Name</param>
        private void doLoad(string strFileName)
        {
            try
            {
                StreamReader reader = new StreamReader(strFileName);
                intNumberOfColumns = int.Parse(reader.ReadLine());
                intNumberOfRows = int.Parse(reader.ReadLine());

                intSquareValue = new int[intNumberOfColumns, intNumberOfRows];

                for (int x = 0; x < intNumberOfColumns; x++)
                {
                    for (int y = 0; y < intNumberOfRows; y++)
                    {
                        intSquareValue[x, y] = int.Parse(reader.ReadLine());
                    }
                }
                createGame(false);
            }
            catch
            {
                MessageBox.Show("Failed to load game", this.Text);
                CallIntroForm();
            }
        }
        /// <summary>
        /// Creates the game
        /// </summary>
        /// <param name="newGame">
        /// Is this a new game or a loaded game.
        /// Returns true if it is a new game
        /// </param>
        private void createGame(bool newGame)
        {
            lblSquare = new Label[intNumberOfColumns, intNumberOfRows];
            if (newGame)
            {
                intSquareValue = new int[intNumberOfColumns, intNumberOfRows];
            }

            for (int x = 0; x < intNumberOfColumns; x++)
            {
                for (int y = 0; y < intNumberOfRows; y++)
                {
                    if (newGame)
                    {
                        if (x == (intNumberOfColumns - 1) && y == (intNumberOfRows - 1))
                        {
                            intSquareValue[x, y] = 0;
                        }
                        else
                        {
                            intSquareValue[x, y] = (x + 1) + y * intNumberOfColumns;
                        }
                    }

                    lblSquare[x, y] = new Label();
                    lblSquare[x, y].Left = int_START_LEFT + int_LENGTH * x;
                    lblSquare[x, y].Top = int_START_TOP + int_LENGTH * y;
                    lblSquare[x, y].Width = int_LENGTH;
                    lblSquare[x, y].Height = int_LENGTH;
                    lblSquare[x, y].BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                    lblSquare[x, y].Font = new Font("Microsoft Sans Serif", int_FONT_SIZE);

                    this.Controls.Add(lblSquare[x, y]);
                    lblSquare[x, y].Click += new EventHandler(lblSquare_Click);
                }
            }

            if (newGame)
            {
                randomize();
            }

            updateGame();
            this.Width = int_LENGTH * intNumberOfColumns + int_START_LEFT + int_RIGHT_SPACE;
            this.Height = int_LENGTH * intNumberOfRows + int_START_TOP + int_BOTTOM_SPACE;
            this.CenterToScreen();
            this.Show();
            this.Activate();
        }
        /// <summary>
        /// Exit menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// Main Menu button
        /// sends user to main menu
        /// any unsaved game will be lost
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuMain_Click(object sender, EventArgs e)
        {
            DialogResult result;
            result = MessageBox.Show("Are you sure you want to quit to main menu?",
                this.Text, MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                clearGame();
                CallIntroForm();
            }

        }

        /// <summary>
        /// Clears the game
        /// </summary>
        private void clearGame()
        {
            for (int x = 0; x < intNumberOfColumns; x++)
            {
                for (int y = 0; y < intNumberOfRows; y++)
                {
                    lblSquare[x, y].Dispose();
                }
            }
        }
        /// <summary>
        /// Form close event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormGame_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.ApplicationExitCall)
            {
                DialogResult result;
                result = MessageBox.Show("Are you sure you want to Exit game?",
                    this.Text, MessageBoxButtons.YesNo);
                if (result == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }
        /// <summary>
        /// Saves the game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuSave_Click(object sender, EventArgs e)
        {
            DialogResult result = saveFileDialog.ShowDialog();
            switch (result)
            {
                case DialogResult.Abort:
                    break;
                case DialogResult.Cancel:
                    break;
                case DialogResult.Ignore:
                    break;
                case DialogResult.No:
                    break;
                case DialogResult.None:
                    break;
                case DialogResult.OK:
                    string strFileName = saveFileDialog.FileName;
                    doSave(strFileName);
                    break;
                case DialogResult.Retry:
                    break;
                case DialogResult.Yes:
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// Saves the game
        /// </summary>
        /// <param name="strFileName">File Name</param>
        void doSave(String strFileName)
        {
            StreamWriter writer = new StreamWriter(strFileName);
            writer.WriteLine(intNumberOfColumns);
            writer.WriteLine(intNumberOfRows);

            for (int x = 0; x < intNumberOfColumns; x++)
            {
                for (int y = 0; y < intNumberOfRows; y++)
                {
                    writer.WriteLine(intSquareValue[x, y]);
                }
            }

            writer.Close();

            MessageBox.Show("Game Saved", this.Text);
        }
        /// <summary>
        /// Updates squares
        /// </summary>
        private void updateGame()
        {
            for (int x = 0; x < intNumberOfColumns; x++)
            {
                for (int y = 0; y < intNumberOfRows; y++)
                {
                    if (intSquareValue[x, y] == 0)
                    {
                        lblSquare[x, y].Text = "";
                    }
                    else
                    {
                        lblSquare[x, y].Text = intSquareValue[x, y].ToString();
                    }
                }
            }

            if (checkForWinner())
            {
                MessageBox.Show("You win", this.Text);
                DialogResult result = MessageBox.Show("Play New Game?", this.Text, MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    clearGame();
                    CallIntroForm();
                }
            }
        }
        /// <summary>
        /// Gets the position for the empty square
        /// </summary>
        /// <param name="intColumn">empty square column</param>
        /// <param name="intRow">empty square row</param>
        private void getEmptySquare(out int intColumn, out int intRow)
        {
            intColumn = 0;
            intRow = 0;

            for (int x = 0; x < intNumberOfColumns; x++)
            {
                for (int y = 0; y < intNumberOfRows; y++)
                {
                    if (intSquareValue[x, y] == 0)
                    {
                        intColumn = x;
                        intRow = y;
                    }
                }
            }
        }
        /// <summary>
        /// Randomize Meathod will move the square randomly
        /// to generate a new game
        /// </summary>
        private void randomize()
        {
            int intNumberOfMovesRemain = (int)((decimal)intNumberOfColumns *
                (decimal)intNumberOfRows * dec_MOVE_COUNTER_TO_NUMBER_OF_SQUARE_RATIO);

            Random random = new Random();
            int idMoveLoation = 0;

            const int int_MIN_VALUE = 1;
            const int int_MAX_VALUE = 4;

            int intEmptyColumn;
            int intEmptyRow;
            getEmptySquare(out intEmptyColumn, out intEmptyRow);

            while (intNumberOfMovesRemain > 0)
            {
                idMoveLoation = random.Next(int_MIN_VALUE, int_MAX_VALUE + 1);
                if (idMoveLoation == (int)move.up)
                {
                    if (intEmptyRow > 0)
                    {
                        intSquareValue[intEmptyColumn, intEmptyRow] =
                            intSquareValue[intEmptyColumn, intEmptyRow - 1];
                        intSquareValue[intEmptyColumn, intEmptyRow - 1] = 0;
                        intEmptyRow--;
                        intNumberOfMovesRemain--;
                    }
                }
                else if (idMoveLoation == (int)move.right)
                {
                    if (intEmptyColumn < (intNumberOfColumns - 1))
                    {
                        intSquareValue[intEmptyColumn, intEmptyRow] =
                            intSquareValue[intEmptyColumn + 1, intEmptyRow];
                        intSquareValue[intEmptyColumn + 1, intEmptyRow] = 0;
                        intEmptyColumn++;
                        intNumberOfMovesRemain--;
                    }
                }
                else if (idMoveLoation == (int)move.down)
                {
                    if (intEmptyRow < (intNumberOfRows - 1))
                    {
                        intSquareValue[intEmptyColumn, intEmptyRow] =
                            intSquareValue[intEmptyColumn, intEmptyRow + 1];
                        intSquareValue[intEmptyColumn, intEmptyRow + 1] = 0;
                        intEmptyRow++;
                        intNumberOfMovesRemain--;
                    }
                }
                else if (idMoveLoation == (int)move.left)
                {
                    if (intEmptyColumn > 0)
                    {
                        intSquareValue[intEmptyColumn, intEmptyRow] =
                            intSquareValue[intEmptyColumn - 1, intEmptyRow];
                        intSquareValue[intEmptyColumn - 1, intEmptyRow] = 0;
                        intEmptyColumn--;
                        intNumberOfMovesRemain--;
                    }
                }
            }
            updateGame();

        }
        /// <summary>
        /// a meathod that checks if user has successfully completed the puzzle or not
        /// </summary>
        /// <returns></returns>
        private bool checkForWinner()
        {
            bool winGame = true;
            for (int x = 0; x < intNumberOfColumns; x++)
            {
                for (int y = 0; y < intNumberOfRows; y++)
                {
                    if (!(x == (intNumberOfColumns - 1) && y == (intNumberOfRows - 1)))
                    {
                        if(intSquareValue[x,y] != (x + 1) + y * intNumberOfColumns)
                        {
                            winGame=false;
                        }
                    }
                }
            }
            return winGame;
        }

        enum move
        {
            up = 1,
            right = 2,
            down = 3,
            left = 4
        }
        /// <summary>
        /// About Menu Clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Created by Brian L. Chaves \r\n"
                + "October 2012", this.Text);
        }
    }
}