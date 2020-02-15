/* Brian Chaves
 * October 18
 * Assinment 3
 * 15 Squares
 * Custom Game dialog
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace bChaves_Assinment_3
{
    public partial class DlgCustom : Form
    {
        int intNumberOfRows = 0;

        public int IntNumberOfRows
        {
            get { return intNumberOfRows; }
            set { intNumberOfRows = value; }
        }

        int intNumberOfColumns = 0;

        public int IntNumberOfColumns
        {
            get { return intNumberOfColumns; }
            set { intNumberOfColumns = value; }
        }

        const int int_MINIMUM_NUMBER_OF_ROWS_AND_COLUMNS = 2;
        const int int_MAXIMUM_NUMBER_OF_ROWS = 10;
        const int int_MAXIMUM_NUMBER_OF_COLUMNS = 19;


        public DlgCustom()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Starts the game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStart_Click(object sender, EventArgs e)
        {
            string strErrorMessage = "";

            //Column check
            if (!(int.TryParse(txtNumberOfColumns.Text, out intNumberOfColumns)))
            {
                strErrorMessage += "Enter a number in the row textbox \r\n";
            }
            else if (intNumberOfColumns < int_MINIMUM_NUMBER_OF_ROWS_AND_COLUMNS)
            {
                strErrorMessage += "Number of columns cannot be less then " +
                    int_MINIMUM_NUMBER_OF_ROWS_AND_COLUMNS.ToString() + " \r\n";
            }
            else if (intNumberOfColumns > int_MAXIMUM_NUMBER_OF_COLUMNS)
            {
                strErrorMessage += "Number of columns cannot be more then " +
                    int_MAXIMUM_NUMBER_OF_COLUMNS.ToString() + " \r\n";
            }

            //Row check
            if (!(int.TryParse(txtNumberOfRows.Text, out intNumberOfRows)))
            {
                strErrorMessage += "Enter a number in the row textbox \r\n";
            }
            else if (intNumberOfRows < int_MINIMUM_NUMBER_OF_ROWS_AND_COLUMNS)
            {
                strErrorMessage += "Number of rows cannot be less then " +
                    int_MINIMUM_NUMBER_OF_ROWS_AND_COLUMNS.ToString() + " \r\n";
            }
            else if (intNumberOfRows > int_MAXIMUM_NUMBER_OF_ROWS)
            {
                strErrorMessage += "Number of rows cannot be more then " +
                    int_MAXIMUM_NUMBER_OF_ROWS.ToString() + " \r\n";
            }


            if (strErrorMessage == "")
            {
                this.Close();
            }
            else
            {
                MessageBox.Show(strErrorMessage, this.Text);
            }
        }
        /// <summary>
        /// Cancle button event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancle_Click(object sender, EventArgs e)
        {
            intNumberOfColumns = -1;
            intNumberOfRows = -1;
            this.Close();
        }
    }
}
