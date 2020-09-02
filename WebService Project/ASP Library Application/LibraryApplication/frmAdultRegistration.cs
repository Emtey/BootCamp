using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Entities;
using System.IO;
using LibraryBusinessTier;
using System.Text.RegularExpressions;


namespace LibraryApplication
{    
    /// <summary>
    /// Add Adult form.
    /// </summary>
    public partial class frmAdultRegistration : Form
    {
        DBInteraction myDb = new DBInteraction();
        AdultMember myAdult = new AdultMember();

        /// <summary>
        /// Constructor of AdultRegistration.
        /// </summary>
        public frmAdultRegistration()
        {
            InitializeComponent();
        }        

        #region Events
        private void frmAdultRegistration_Load(object sender, EventArgs e)
        {
            DataSet myDataset = new DataSet();
            StringReader myReader = new StringReader(Properties.Resources.States);
            myDataset.ReadXml(myReader, XmlReadMode.InferSchema);
            cbState.DataSource = myDataset.Tables[0];
            cbState.DisplayMember = "Abbreviation";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (this.ValidateChildren())
            {
                myAdult.FirstName = txtFirstName.Text;
                myAdult.MiddleInitial = txtMidInit.Text;
                myAdult.LastName = txtLastName.Text;
                myAdult.Street = txtStreet.Text;
                myAdult.City = txtCity.Text;
                myAdult.State = cbState.Text;
                myAdult.ZipCode = txtZip.Text;
                myAdult.PhoneNumber = txtPhone.Text;

                try
                {
                    myDb.AddMember(myAdult);
                    frmMemberInfo memberInfo = new frmMemberInfo((Member)myAdult);
                    memberInfo.MdiParent = this.MdiParent;
                    memberInfo.Show();
                    this.Close();
                }
                catch (LibraryException ex)
                {
                    if (ex.LibraryErrorCode == ErrorCode.AddAdultFailed)
                    {
                        string myErr = string.Format("Add Adult Failed");
                        tsslLabel.Text = myErr;
                        return;
                    }
                    if (ex.LibraryErrorCode == ErrorCode.GenericException)
                    {
                        tsslLabel.Text = "ERROR: " + ex.Message;
                        return;
                    }
                }
            }
        }       

        #region Validating Events
        private void txtFirstNameText_Validating(object sender, CancelEventArgs e)
        {
            if (!Regex.IsMatch(txtFirstName.Text.ToString(), "^[A-Z][a-z]{0,14}$"))
            {
                errorProvider1.SetError(txtFirstName, "Valid format is character upper case subsequent characters lower case, with a length of no more than 15 characters..");
                tsslStatusLabel.Text = "Please correct all errors.";
                e.Cancel = true;
            }         
        }
        
        private void txtLastName_Validating(object sender, CancelEventArgs e)
        {
            if (!Regex.IsMatch(txtLastName.Text.ToString(), "^[A-Z][a-z]{0,14}$"))
            {
                errorProvider1.SetError(txtLastName, "Valid format is character upper case subsequent characters lower case, with a length of no more than 15 characters..");
                tsslStatusLabel.Text = "Please correct all errors.";
                e.Cancel = true;
            }
        }

        private void txtStreet_Validating(object sender, CancelEventArgs e)
        {
            if (txtStreet.Text.ToString().Trim().Length == 0)
            {
                errorProvider1.SetError(txtStreet, "Street address is required, with a length of no more than 15 characters.");
                tsslStatusLabel.Text = "Please correct all errors.";
                e.Cancel = true;
            }
        }

        private void txtCity_Validating(object sender, CancelEventArgs e)
        {
            if (txtCity.Text.ToString().Trim().Length == 0)
            {
                errorProvider1.SetError(txtCity, "City is required, with a length of no more than 15 characters.");
                tsslStatusLabel.Text = "Please correct all errors.";
                e.Cancel = true;
            }
        }

        private void cbState_Validating(object sender, CancelEventArgs e)
        {
            if (!Regex.IsMatch(cbState.Text.ToString(), "^[A-Z]{0,2}$"))
            {
                errorProvider1.SetError(cbState, "Valid format is two upper case chars.");
                tsslStatusLabel.Text = "Please correct all errors.";
                e.Cancel = true;
            }
        }

        private void txtZip_Validating(object sender, CancelEventArgs e)
        {
            if (!Regex.IsMatch(txtZip.Text.ToString(), @"^\d{5}(-\d{4})?$"))
            {
                errorProvider1.SetError(txtZip, "Enter the five digit zip code or the zip + 4 with a hyphen separator. (#####-####)");
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

        private void txtPhone_Validating(object sender, CancelEventArgs e)
        {
            if (txtPhone.Text.ToString() != string.Empty)
            {
                if (!Regex.IsMatch(txtPhone.Text.ToString(), @"^\(\d{3}\)\d{3}-\d{4}$"))
                {
                    errorProvider1.SetError(txtPhone, "Format is (###)###-####.");
                    tsslStatusLabel.Text = "Enter a valid phone number.";
                    e.Cancel = true;
                }
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

        private void frmAdultRegistration_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false;
        }

        #endregion
    }      
}