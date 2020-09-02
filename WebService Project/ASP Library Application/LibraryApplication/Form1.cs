using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
//using SetFocus.Library;
using LibraryDataAccess;

namespace LibraryApplication
{
    public partial class frmMain : Form
    {
        /// <summary>
        /// Main form that is displayed.
        /// </summary>
        public frmMain()
        {
            InitializeComponent();
            arrangeIconsToolStripMenuItem.Enabled = false;
            arrangeIconsToolStripMenuItem.Visible = false;
            //center on the screen.
            CenterToScreen();
            //open the member info panel on load.
            Form memberInfo = new frmMemberInfo();
            memberInfo.MdiParent = this;
            memberInfo.Show();
        }       

        #region File Menu Item
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //shut it down!
            this.Close();
        }
        #endregion
        
        #region Member Services Menu Item
        private void getMemberInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //create a new member info panel, set its parent to form1 (this)
            //and open the bad boy!
            Form memberInfo = new frmMemberInfo();
            memberInfo.MdiParent = this;
            memberInfo.Show();
        }

        private void addAdultToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //create a new Adult Registration panel, set its parent to form1(this)
            //and open the bad boy!
            Form adultReg = new frmAdultRegistration();
            adultReg.MdiParent = this;
            adultReg.Show();
        }

        private void addJuvenileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form juveReg = new frmJuvenileRegistration();
            juveReg.MdiParent = this;
            juveReg.Show();
        }

        private void checkInToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form checkIn = new frmCheckIn(true);
            checkIn.MdiParent = this;
            checkIn.Show();
        }

        //private void checkOutToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    //Form activeForm = this.ActiveMdiChild;
        //    //frmCheckIn checkPanel;
        //    //frmMemberInfo myForm;
        //    //if (activeForm is frmMemberInfo)
        //    //{
        //    //    myForm = (frmMemberInfo)activeForm;
        //    //    checkPanel = new frmCheckIn(false, Convert.ToInt16(myForm.txtMemberID.Text));
        //    //}
        //    //else
        //    //    checkPanel = new frmCheckIn(false);
        //    //checkPanel.MdiParent = this;
        //    //checkPanel.Show();
        //}
        #endregion

        #region Windows Menu Item
        private void cascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.Cascade);
        }

        private void tileVerticallyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileVertical);
        }

        private void tileHorizontallyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void closeAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form x in this.MdiChildren)
                x.Close();
        }

        private void arrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.ArrangeIcons);
        }
        #endregion

        #region About Menu Item
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //call the new About form.
            Form about = new frmAbout();
            about.MdiParent = this;
            about.Show();

        }
        #endregion

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false;
        }
    }
}