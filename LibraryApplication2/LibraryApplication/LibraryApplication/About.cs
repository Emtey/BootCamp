using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LibraryApplication
{
    /// <summary>
    /// The form that is displayed when the user selects About from the main menu.
    /// </summary>
    public partial class frmAbout : Form
    {
        /// <summary>
        /// Constructor of frmAbout.  Right now its loading the text for the about panel.
        /// </summary>
        public frmAbout()
        {
            InitializeComponent();
            rtbAbout.AppendText("\t\tLibrary Application\n");
            rtbAbout.AppendText("\t\tCreated 2/9/2007\n");
            rtbAbout.AppendText("\t\tBy:  Mark Simpson");
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}