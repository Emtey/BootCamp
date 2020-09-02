using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Entities;
using LibraryBusinessTier;
using System.Text;
using System.Drawing;

public partial class CheckInPanel : System.Web.UI.Page
{
    Item myItem;
    DBInteraction myDb = new DBInteraction();
    Member myMember;

    protected void Page_Load(object sender, EventArgs e)
    {
    }
    protected void btnCheckIn_Click(object sender, EventArgs e)
    {
        try
        {
            int myIsbn = Convert.ToInt32(txtISBN.Text);
            short myCopyNum = Convert.ToInt16(txtCopyNum.Text);

            myItem = myDb.GetItem(myIsbn, myCopyNum);

            if (myItem.MemberNumber == 0)  //item is not checked out. 
            {
                StringBuilder myString = new StringBuilder();
                myString.Append("\"");
                myString.Append(myItem.Title.ToString());
                myString.Append("\" Is not on loan.");
                lblStatus.Text = myString.ToString();
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
                lblStatus.Text = myString.ToString();

                btnFinalCheckIn.Visible = true;
                btnCancel.Visible = true;
                btnCheckIn.Visible = false;
               
                //lock the fields from being edited
                txtCopyNum.ReadOnly = true;
                txtISBN.ReadOnly = true;
            }
            Label lbl = (Label)Page.Master.FindControl("lblStatus");
            lbl.Text = string.Empty;
        }
        catch (LibraryException ex)
        {
            if (ex.LibraryErrorCode == ErrorCode.ItemNotFound)
            {
                StringBuilder myString = new StringBuilder();
                myString.Append("Item not found");
                Label lbl = (Label)Page.Master.FindControl("lblStatus");
                lbl.Text = myString.ToString();
                lbl.ForeColor = Color.Red;

            }
            else if (ex.LibraryErrorCode == ErrorCode.ItemNotOnLoan)
            {
                Label lbl = (Label)Page.Master.FindControl("lblStatus");
                lbl.Text = "Item not checked out.";
                lbl.ForeColor = Color.Red;
            }
            else
            {
                StringBuilder myString = new StringBuilder();
                myString.Append("An error was encountered on check in. ");
                myString.Append(ex.Message);
                Label lbl = (Label)Page.Master.FindControl("lblStatus");
                lbl.Text = myString.ToString();
                lbl.ForeColor = Color.Red;
            }
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //reset screen
        btnFinalCheckIn.Visible = false;
        btnCancel.Visible = false;
        txtISBN.ReadOnly = false;
        txtCopyNum.ReadOnly = false;
        txtISBN.Text = string.Empty;
        txtCopyNum.Text = string.Empty;
        btnCheckIn.Visible = true;
        lblStatus.Text = string.Empty;
        Label lbl = (Label)Page.Master.FindControl("lblStatus");
        lbl.Text = "Check in cancelled by user.";
    }
    protected void btnFinalCheckIn_Click(object sender, EventArgs e)
    {
        //do the actual check in stuff
        try
        {
            myDb.CheckInItem(Convert.ToInt32(txtISBN.Text), Convert.ToInt16(txtCopyNum.Text));
            Label lbl = (Label)Page.Master.FindControl("lblStatus");
            lbl.Text = "Check in successful!";
            lbl.ForeColor = Color.Blue;

            lblStatus.Text = string.Empty;
            txtISBN.Text = string.Empty;
            txtCopyNum.Text = string.Empty;
            txtCopyNum.ReadOnly = false;
            txtISBN.ReadOnly = false;
            btnCancel.Visible = false;
            btnCheckIn.Visible = true;
            btnFinalCheckIn.Visible = false;
        }
        catch (LibraryException ex)
        {
            if (ex.LibraryErrorCode == ErrorCode.CheckInFailed)
            {
                Label lbl = (Label)Page.Master.FindControl("lblStatus");
                lbl.Text = "Check in Failed!";
                lbl.ForeColor = Color.Red;
            }
            else
            {
                StringBuilder myString = new StringBuilder();
                myString.Append("An error was encountered on check in. ");
                myString.Append(ex.Message);
                Label lbl = (Label)Page.Master.FindControl("lblStatus");
                lbl.Text = myString.ToString();
                lbl.ForeColor = Color.Red;
            }
        }
    }
}
