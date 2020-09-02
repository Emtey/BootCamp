using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SetFocus.Library.DataAccess;
using SetFocus.Library.Entities;
using LibraryBusinessTier;

namespace LibraryApplication
{
    /// <summary>
    /// Check in window.
    /// </summary>
    public partial class frmCheckIn : Form
    {
        DBInteraction myDb = new DBInteraction();
        
        Item myItem;
        Member myMember;

        short memberID;
        private bool isCheckIn = true;

        #region Constructors
        /// <summary>
        /// Check in form constructor.
        /// </summary>
        public frmCheckIn()
        {
            InitializeComponent();
            CenterToScreen();
        }
        /// <summary>
        /// Constructor for the Check In/Out form.
        /// </summary>
        /// <param name="isCheckIn">If true, then display the check in panel, otherwise display check out panel.</param>
        public frmCheckIn(bool isCheckIn)
        {
            InitializeComponent();
            CenterToScreen();
            //if isCheckIn is false, then we have a check out operation, mod the panel appropriately.
            if (!isCheckIn)
            {
                this.Text = "Check Out";
                btnCheckIn.Text = "Check Out";
                this.isCheckIn = isCheckIn;
            }
        }
        /// <summary>
        /// Constructor for the Check in/out form
        /// </summary>
        /// <param name="isCheckIn">If true, then display the check in panel, otherwise display check out panel.</param>
        /// <param name="memberID">The current member's memberID.</param>
        public frmCheckIn(bool isCheckIn, short memberID)
        {
            InitializeComponent();
            CenterToScreen();
            if (!isCheckIn)
            {
                this.Text = "Check Out";
                btnCheckIn.Text = "Check Out";
                this.isCheckIn = isCheckIn;
                this.memberID = memberID;
            }
        }
        #endregion

        #region Events
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCheckIn_Click(object sender, EventArgs e)
        {
            if (this.ValidateChildren())
            {
                int myIsbn = Convert.ToInt32(txtISBN.Text);
                short myCopyNum = Convert.ToInt16(txtCopyNum.Text);

                if (!isCheckIn)
                {
                    //we are checking out.
                    CheckOut(myIsbn, myCopyNum);
                }
                else
                    //we are checking in.
                    CheckIn(memberID, myIsbn, myCopyNum);
            }   
        }

        private void frmCheckIn_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false;
        }

        #region Validating Events
        private void txtISBN_Validating(object sender, CancelEventArgs e)
        {
            if (!Helpers.StringToIntVal(txtISBN.Text))
            {
                tsslStatusLabel.Text = "Please correct all errors.";
                string myString = string.Format("ISBN must be between 1 and {0}", Int32.MaxValue);
                errorProvider1.SetError(txtISBN, myString);
                e.Cancel = true;
            }
        }

        private void txtCopyNum_Validating(object sender, CancelEventArgs e)
        {
            if (!Helpers.StringToShortVal(txtCopyNum.Text))
            {
                tsslStatusLabel.Text = "Please correct all errors.";
                string myString = string.Format("Copy Number must be between 1 and {0}", Int16.MaxValue);
                errorProvider1.SetError(txtCopyNum, myString);
                e.Cancel = true;
            }
        }
        #endregion

        #region Validated Event
        private void txtField_Validated(object sender, EventArgs e)
        {
            this.errorProvider1.SetError((Control)sender, string.Empty);
            this.tsslStatusLabel.Text = string.Empty;
        }
        #endregion
        #endregion

        #region CheckOut
        //perform the check out of the book.
        private void CheckOut(int myIsbn, short myCopyNum)
        {
            try
            {
                myItem = myDb.GetItem(myIsbn, myCopyNum);
                myDb.CheckOutItem(memberID, myIsbn, myCopyNum);
                DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (LibraryException ex)
            {
                //myItem = myDb.GetItem(myIsbn, myCopyNum);

                if (ex.LibraryErrorCode == ErrorCode.CheckOutFailed)
                {
                    StringBuilder myString = new StringBuilder();
                    myString.Append("Could not check out: \"");
                    myString.Append(myItem.Title);
                    myString.Append("\"");
                    errorProvider1.SetError(txtISBN, "Check Out Failed");
                    tsslStatusLabel.Text = myString.ToString();
                    txtISBN.Focus();
                }
                else if (ex.LibraryErrorCode == ErrorCode.ItemAlreadyOnLoan)
                {
                    StringBuilder myString = new StringBuilder();
                    myString.Append("Item is on loan to ");
                    Member temp = myDb.GetMember(myItem.MemberNumber);
                    myString.Append(temp.FirstName + " " + temp.LastName + " (" + temp.MemberID.ToString());
                    myString.Append("), Check in, and check out?");
                    DialogResult button = MessageBox.Show(myString.ToString(), "Check in first?", MessageBoxButtons.YesNo);

                    if (button == DialogResult.Yes)
                    {
                        myDb.CheckInItem(myIsbn, myCopyNum);
                        CheckOut(myIsbn, myCopyNum);
                    }
                }                
                else if (ex.LibraryErrorCode == ErrorCode.ItemNotFound)
                {
                    string myErr = string.Format("Item Not Found.");
                    errorProvider1.SetError(txtISBN, "Item Not Found.");
                    tsslStatusLabel.Text = myErr;
                    txtISBN.Focus();
                }
                else 
                {
                    string myErr = string.Format("ERROR: " + ex.Message);
                    errorProvider1.SetError(txtISBN, "Check out failed.");
                    tsslStatusLabel.Text = myErr;
                    txtISBN.Focus();
                }
            }
        }
        #endregion

        #region CheckIn
        private void CheckIn(short memberId, int myIsbn, short myCopyNum)
        {
            try
            {
                myItem = myDb.GetItem(myIsbn, myCopyNum);

                if (myItem.MemberNumber == 0)  //item is not checked out. 
                {
                    StringBuilder myString = new StringBuilder();
                    myString.Append("\"");
                    myString.Append(myItem.Title.ToString());
                    myString.Append("\" Is not on loan.");
                    tsslStatusLabel.Text = myString.ToString();
                }
                else
                {
                    myMember = myDb.GetMember(myItem.MemberNumber);
                    StringBuilder myString = new StringBuilder();

                    myString.Append("Check in \"");
                    myString.Append(myItem.Title.ToString());
                    myString.Append("\" by ");
                    myString.Append(myItem.Author.ToString());
                    myString.Append(" on loan to ");
                    myString.Append(myMember.FirstName);
                    myString.Append(" ");
                    myString.Append(myMember.LastName);
                    myString.Append("(");
                    myString.Append(myMember.MemberID);
                    myString.Append(")");
                    DialogResult button = MessageBox.Show(myString.ToString(), "Confirm Check In", MessageBoxButtons.YesNo);

                    if (button == DialogResult.No)
                    {
                        tsslStatusLabel.Text = "Check in process cancelled by user.";
                    }
                    else
                    {

                        myDb.CheckInItem(myIsbn, myCopyNum);
                        tsslStatusLabel.Text = "Check in successful.";
                        txtISBN.Text = string.Empty;
                        txtCopyNum.Text = string.Empty;
                        txtISBN.Focus();
                    }
                }
            }
            catch (LibraryException ex)
            {
                if (ex.LibraryErrorCode == ErrorCode.ItemNotFound)
                {
                    StringBuilder myString = new StringBuilder();
                    myString.Append("Item not found");
                    errorProvider1.SetError(txtISBN, "Item not found, check values and retry.");
                    tsslStatusLabel.Text = myString.ToString();
                    txtISBN.Focus();

                }
                if (ex.LibraryErrorCode == ErrorCode.ItemNotOnLoan)
                {
                    tsslStatusLabel.Text = "Item not checked out.";
                }
                else
                {
                    StringBuilder myString = new StringBuilder();
                    myString.Append("An error was encountered on check in. ");
                    myString.Append(ex.Message);
                    tsslStatusLabel.Text = myString.ToString();
                }
            }
        }
        #endregion
    }
}
