using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Entities;
using LibraryDataAccess;
using LibraryBusinessTier;
using System.Text.RegularExpressions;

namespace LibraryApplication
{
    /// <summary>
    /// Juvenile Registration form.
    /// </summary>
    public partial class frmJuvenileRegistration : Form
    {
        DBInteraction myDb = new DBInteraction();
        JuvenileMember myJuve = new JuvenileMember();

        /// <summary>
        /// Constructor for frmJuvenileRegistration.
        /// </summary>
        public frmJuvenileRegistration()
        {
            InitializeComponent();
        }

        #region Events
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (this.ValidateChildren())
            {
                myJuve.BirthDate = Convert.ToDateTime(dtpBirthDate.Text);
                myJuve.FirstName = txtFirstName.Text;
                myJuve.MiddleInitial = txtMidInit.Text;
                myJuve.LastName = txtLastName.Text;
                myJuve.AdultMemberID = Convert.ToInt16(txtAdultMemberID.Text);

                try
                {
                    myDb.AddMember(myJuve);
                    frmMemberInfo memberInfo = new frmMemberInfo((Member)myJuve);
                    memberInfo.MdiParent = this.MdiParent;
                    memberInfo.Show();
                    this.Close();
                }
                catch (LibraryException)
                {
                    string myErr = string.Format("Add Juvenile Failed");
                    tsslStatusLabel.Text = myErr;
                    return;
                }
            }
        }         

        #region Validating Events
        private void txtFirstName_Validating(object sender, CancelEventArgs e)
        {
            if (!Regex.IsMatch(txtFirstName.Text.ToString(),"^[A-Z][a-z]{0,14}$"))
            {
                errorProvider1.SetError(txtFirstName, "Valid format is character upper case subsequent characters lower case.");
                tsslStatusLabel.Text = "Please correct all errors.";
                e.Cancel = true;
            }
        }

        private void txtMidInit_Validating(object sender, CancelEventArgs e)
        {
            if (!Regex.IsMatch(txtMidInit.Text.ToString(), "^[A-Z]{0,1}$"))
            {
                errorProvider1.SetError(txtMidInit, "Valid format is a single upper case charactrer.");
                tsslStatusLabel.Text = "Please correct all errors.";
                e.Cancel = true;
            }
        }

        private void txtLastName_Validating(object sender, CancelEventArgs e)
        {
            if (!Regex.IsMatch(txtLastName.Text.ToString(), "^[A-Z][a-z]{0,14}$"))
            {
                errorProvider1.SetError(txtLastName, "Valid format is character upper case subsequent characters lower case.");
                tsslStatusLabel.Text = "Please correct all errors.";
                e.Cancel = true;
            }
        }

        private void txtAdultMemberID_Validating(object sender, CancelEventArgs e)
        {
            if (!Helpers.IsNumeric(txtAdultMemberID.Text.ToString()))
            {
                string myString = string.Format("Member IDs are values that range from 1 to {0}", Int16.MaxValue);
                errorProvider1.SetError(txtAdultMemberID, myString.ToString());
                tsslStatusLabel.Text = "Please correct all errors.";
                e.Cancel = true;
            }
            else
            {
                try
                {
                    Member myMember = myDb.GetMember(Convert.ToInt16(txtAdultMemberID.Text));
                }
                catch (LibraryException )
                {
                    string myErr = string.Format("Member ID: {0} was not found.", txtAdultMemberID.Text);
                    errorProvider1.SetError(txtAdultMemberID, myErr.ToString());
                    tsslStatusLabel.Text = "Please correct all errors.";
                    e.Cancel = true;
                }
                catch (Exception ex)
                {
                    string myErr = string.Format("ERROR: " + ex.Message );
                    errorProvider1.SetError(txtAdultMemberID, myErr.ToString());
                    tsslStatusLabel.Text = myErr;
                    e.Cancel = true;
                }
            }
        }

        private void dtpBirthDate_Validating(object sender, CancelEventArgs e)
        {
            DateTime myDateTime = Convert.ToDateTime(dtpBirthDate.Text.ToString());
            if (DateTime.Now.AddYears(-18) > myDateTime)
            {
                errorProvider1.SetError(dtpBirthDate, "Person is older than 18, enter as adult member.");
                tsslStatusLabel.Text = "Please correct all errors.";
                e.Cancel = true;
            }
        }
        #endregion

        #region Validated Event
        private void txtField_Validated(object sender, EventArgs e)
        {
            this.errorProvider1.SetError((Control)sender, string.Empty);
            tsslStatusLabel.Text = string.Empty;
        }
        #endregion

        private void frmJuvenileRegistration_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false;
        }

        #endregion

    }
}