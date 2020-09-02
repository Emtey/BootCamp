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
using System.Text.RegularExpressions;

namespace LibraryApplication
{
    /// <summary>
    /// Member Info window
    /// </summary>
    public partial class frmMemberInfo : Form
    {   
        DBInteraction myDb = new DBInteraction();
        Member myMember = new Member();
        JuvenileMember juve = new JuvenileMember();

        bool RowCheckIn = false;
        bool isExpired = false;

        #region Constructors
        /// <summary>
        /// Member Info Constructor
        /// </summary>
        public frmMemberInfo()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Constructor that takes a Member argument.
        /// </summary>
        /// <param name="m">Member</param>
        public frmMemberInfo(Member m)
        {
            InitializeComponent();

            LibraryDataAccess myAccess = new LibraryDataAccess();
            Member myMember = myAccess.GetMember(m.MemberID);
            LoadGenericFields(myMember);
            if (myMember is JuvenileMember)
                LoadJuvenileFields(myMember);
            else
                FormatAdultWindow();
            this.Text = myMember.FirstName + " " + myMember.MiddleInitial + " " + myMember.LastName + "(" + myMember.MemberID + ")";
            GridRefresh(m.MemberID);
            tsslLabel.Text = "Member created.";
        }
        #endregion

        #region Events
        //Get Info button event.
        private void btnGetInfo_Click(object sender, EventArgs e)
        {
            //get the valid member or return an error.
            try
            {
                if (this.ValidateChildren())
                {
                    myMember = myDb.GetMember(Convert.ToInt16(txtMemberID.Text));
                    LoadGenericFields(myMember);
                    if (myMember is JuvenileMember)
                        LoadJuvenileFields(myMember);
                    else
                        FormatAdultWindow();

                    tsslLabel.Text = string.Empty;

                    if ((DateTime)myMember.ExpirationDate.Date < DateTime.Now.Date)
                    {
                        txtExpDate.BackColor = Color.Red;
                        tsslLabel.Text = "Membership has expired.";
                        isExpired = true;
                    }
                    else if ((DateTime)myMember.ExpirationDate.Date < DateTime.Now.Date.AddDays(14) &&
                        (DateTime)myMember.ExpirationDate.Date == DateTime.Now.Date)
                    {
                        txtExpDate.BackColor = Color.Yellow;
                        tsslLabel.Text = "Membership set to expire in 14 days.";
                        isExpired = false;
                    }
                    

                    this.Text = myMember.FirstName + " " + myMember.MiddleInitial + " " + myMember.LastName + "(" + myMember.MemberID + ")";
                    //now that we have a valid member, lets try to get any books 
                    //that are checked out.
                    GridRefresh(myMember.MemberID);
                }
            }
            catch (LibraryException)
            {
                string myErr = string.Format("Member ID: {0} was not found.", txtMemberID.Text);
                tsslLabel.Text = myErr;
                txtMemberID.Focus();
                return;
            }
        }

        //Check In button click event.
        private void btnCheckIn_Click(object sender, EventArgs e)
        {
            //set this bool to turn off the data grid refresh.
            RowCheckIn = true;
            foreach (DataGridViewRow row in itemsDataGridView.Rows)
            {
                if (row.Selected)
                {
                    //for every row that is selected attempt to check in the book.
                    try
                    {
                        DataGridViewCell isbnCell = row.Cells[dgvIsbn.Index];
                        DataGridViewCell copyNumCell = row.Cells[dgvCopyNum.Index];

                        int isbn = (int)isbnCell.Value;
                        short copyNum = (short)copyNumCell.Value;

                        myDb.CheckInItem(isbn, copyNum);
                    }
                    catch (LibraryException)
                    {
                        string myErr = string.Format("Error on check in");
                        tsslLabel.Text = myErr;
                        txtMemberID.Focus();
                        return;
                    }
                }
            }
            RowCheckIn = false;
            GridRefresh(Convert.ToInt16(txtMemberID.Text));
        }

        private void checkOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form checkIn = new frmCheckIn(false, Convert.ToInt16(txtMemberID.Text));
            DialogResult dr = checkIn.ShowDialog(this);
            if (DialogResult.OK == dr)
            {
                tsslLabel.Text = "Check out successful";
            }
        }

        private void frmMemberInfo_FormClosing(object sender, FormClosingEventArgs e)
        {
            DBInteraction.NewItemAdded -= new ItemsEventDelegate(DBInteraction_NewItemAdded);
            e.Cancel = false;
        }

        #region Event wirings.
        //hook up to the event
        private void frmMemberInfo_Load(object sender, EventArgs e)
        {
            DBInteraction.NewItemAdded += new ItemsEventDelegate(DBInteraction_NewItemAdded);
        }
        //event handler for the event on this form.
        void DBInteraction_NewItemAdded(ItemsEventsArgs args)
        {
            if (this.ValidateChildren())
            {
                if (Convert.ToInt16(txtMemberID.Text) == args.memberID)
                {
                    GridRefresh(args.memberID);
                }
            }
        }
        #endregion

        #region Validating Events
        //Validating event.
        private void txtMemberID_Validating(object sender, CancelEventArgs e)
        {
            if (!Helpers.IsNumeric(txtMemberID.Text.ToString()))
            {
                string myString = string.Format("Member IDs are values that range from 1 to {0}", Int16.MaxValue);
                errorProvider1.SetError(txtMemberID, myString.ToString());
                tsslLabel.Text = "Invalid Member ID Range";
                e.Cancel = true;
            }
            else if (!Helpers.StringToShortVal(txtMemberID.Text.ToString()))
            {
                string myString = string.Format("Invalid Member range, values range from 1 to {0}", Int16.MaxValue);
                errorProvider1.SetError(txtMemberID, myString.ToString());
                tsslLabel.Text = "Invalid Member ID Range";
                e.Cancel = true;
            }
            else
            {
                try
                {
                    Member myMember = myDb.GetMember(Convert.ToInt16(txtMemberID.Text));
                }
                catch (LibraryException)
                {
                    string myErr = string.Format("Member ID: {0} was not found.", txtMemberID.Text);
                    errorProvider1.SetError(txtMemberID, myErr.ToString());
                    tsslLabel.Text = myErr;
                    e.Cancel = true;
                }
                catch (Exception)
                {
                    string myErr = string.Format("Invalid Member range, values range from 1 to {0}", Int16.MaxValue);
                    errorProvider1.SetError(txtMemberID, myErr.ToString());
                    tsslLabel.Text = myErr;
                    e.Cancel = true;
                }
            }
        }
        #endregion 

        #region Validated Event
        private void txtMemberID_Validated(object sender, EventArgs e)
        {
            this.errorProvider1.SetError((Control)sender, string.Empty);
            this.tsslLabel.Text = string.Empty;
        }
        #endregion        
        #endregion

        #region Field Load helper methods
        //Loads the common fields Juvenile and Adult members share.
        private void LoadGenericFields(Member myMember)
        {
            txtMemberID.Text = myMember.MemberID.ToString();
            txtExpDate.Text = myMember.ExpirationDate.ToString("MM/dd/yyyy");
            txtFirstName.Text = myMember.FirstName;
            txtLastName.Text = myMember.LastName;
            txtMidInit.Text = myMember.MiddleInitial;
            txtStreet.Text = myMember.Street;
            txtCity.Text = myMember.City;
            txtState.Text = myMember.State;
            txtZip.Text = myMember.ZipCode;
            txtPhone.Text = myMember.PhoneNumber;
        }
        //loads the fields that are common to Juvenile members.
        private void LoadJuvenileFields(Member myMember)
        {
            juve = (JuvenileMember)myMember;

            //set the fields visibles
            lblBirth.Visible = true;
            lblAdultID.Visible = true;
            txtBirthDate.Visible = true;
            txtAdultMemberID.Visible = true;

            //move the myMemberfields into the txt fields.            
            txtAdultMemberID.Text = juve.AdultMemberID.ToString();
            txtBirthDate.Text = juve.BirthDate.ToString("MM/dd/yyyy");
        }
        //Hides the Juvenile fields that we dont need to see for adults.
        private void FormatAdultWindow()
        {
            //set the fields visibles to false if they were true
            lblBirth.Visible = false;
            lblAdultID.Visible = false;
            txtBirthDate.Visible = false;
            txtAdultMemberID.Visible = false;
        }
        #endregion
          
        //Call this method to refresh the DataGridView.
        private void GridRefresh(short memberID)
        {
            if (!RowCheckIn)
            {
                try
                {
                    ItemsDataSet myItems = myDb.GetItems(memberID);
                    itemsBindingSource.DataSource = myItems;

                    if (myItems.Tables[0].Rows.Count > 3 || isExpired)
                        checkOutToolStripMenuItem.Enabled = false;
                    else
                        checkOutToolStripMenuItem.Enabled = true;
                }
                catch (LibraryException)
                {
                    string myErr = string.Format("Error on grid refresh.");
                    tsslLabel.Text = myErr;
                    txtMemberID.Focus();
                    return;
                }
            }
        }        
    }
}