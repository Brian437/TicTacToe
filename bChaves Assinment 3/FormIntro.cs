/* Brian Chaves
 * October 18
 * Assinment 3
 * 15 Squares
 * Intro Form
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
    public partial class FormIntro : Form
    {
        string strGameMode = "";

        public string StrGameMode
        {
            get { return strGameMode; }
            set { strGameMode = value; }
        }

        public FormIntro()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Start original game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOriginal_Click(object sender, EventArgs e)
        {
            strGameMode = "original";
            this.Close();
        }
        /// <summary>
        /// Enters custom game dialog
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCustom_Click(object sender, EventArgs e)
        {
            strGameMode = "custom";
            this.Close();
        }
        /// <summary>
        /// Loads game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLoad_Click(object sender, EventArgs e)
        {
            strGameMode = "load game";
            this.Close();
        }
        /// <summary>
        /// Exit Game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, EventArgs e)
        {
            strGameMode = "quit";
            this.Close();
        }
        /// <summary>
        /// This code prevents the form from hiding behide other application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormIntro_Load(object sender, EventArgs e)
        {
            this.Activate();
        }

        /// <summary>
        /// Logo click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picLogo_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Created by Brian L. Chaves \r\n"
                + "October 2012", this.Text);
        }
    }
}
