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
using System.Text;
using System.Drawing;
using System.Web.Services.Protocols;
using ExceptionCodeLibrary;
using System.Net;

using LibraryWebService;
//using LibraryBusinessTier;

/// <summary>
/// CheckOutPanel
/// </summary>
public partial class CheckOutPanel : System.Web.UI.Page
{
    short memberID;
    //Item myItem;
    //DBInteraction myDb = new DBInteraction();
    LibraryWebService.Item myItem;
    LibraryWebService.ServiceWse myDb = new LibraryWebService.ServiceWse();

    protected void Page_Load(object sender, EventArgs e)
    {
        myDb.SetPolicy("LibraryClientPolicy");
        memberID = Convert.ToInt16(Session["MemberID"]);
    }
    protected void btnCheckOut_Click(object sender, EventArgs e)
    {
        try
        {
            int myIsbn = Convert.ToInt32(txtISBN.Text);
            short myCopyNum = Convert.ToInt16(txtCopyNum.Text);

            myItem = myDb.GetItem(myIsbn, myCopyNum);
            StringBuilder myString = new StringBuilder();

            if (OnLoan(myItem))
            {
                StringBuilder myStr = new StringBuilder();
                myStr.Append("Item is on loan to ");
                LibraryWebService.Member temp = myDb.GetMember(myItem.MemberNumber);
                myStr.Append(temp.FirstName + " " + temp.LastName + " (" + temp.MemberID.ToString());
                myStr.Append("), Check in, and check out?");
                Label lbl = (Label)Page.Master.FindControl("lblStatus");
                lbl.Text = myStr.ToString();
            }

            myString.Append("Check Out \"");
            myString.Append(myItem.Title.ToString());
            myString.Append("\" by ");
            myString.Append(myItem.Author.ToString());
            myString.Append(" ?");

            lblStatus.Text = myString.ToString();

            //lock the fields for editing
            txtISBN.ReadOnly = true;
            txtCopyNum.ReadOnly = true;
            btnCancel.Visible = true;
            btnCheckOut.Visible = false;
            btnFinalCheckOut.Visible = true;
        }
        catch (SoapException ex)
        {
            if (ex.Code == ExceptionCodes.ItemNotFound)
            {
                string myString = string.Format("Item Not Found.");
                Label lbl = (Label)Page.Master.FindControl("lblStatus");
                lbl.Text = myString;
                lbl.ForeColor = Color.Red;
            }            
            else
            {
                string myString = string.Format("ERROR: An error was encountered on check in.");
                Label lbl = (Label)Page.Master.FindControl("lblStatus");
                lbl.Text = myString;
                lbl.ForeColor = Color.Red;
            }
        }
        catch (WebException)
        {
            Label lbl = (Label)Page.Master.FindControl("lblStatus");
            lbl.Text = "Web service is not responding, please try again later.";
            lbl.ForeColor = Color.Red;
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //reset screen
        btnCheckOut.Visible = true;
        btnFinalCheckOut.Visible = false;
        btnCancel.Visible = false;
        txtISBN.ReadOnly = false;
        txtISBN.Text = string.Empty;
        txtCopyNum.ReadOnly = false;
        txtCopyNum.Text = string.Empty;
        
        lblStatus.Text = string.Empty;
        Label lbl = (Label)Page.Master.FindControl("lblStatus");
        lbl.Text = "Check out cancelled by user.";
    }
    protected void btnFinalCheckOut_Click(object sender, EventArgs e)
    {
        //do the actual check out stuff
        try
        {
            int myIsbn = Convert.ToInt32(txtISBN.Text);
            short myCopyNum = Convert.ToInt16(txtCopyNum.Text);

            if (OnLoan(myDb.GetItem(myIsbn, myCopyNum)))
            {
                myDb.CheckInItem(myIsbn,myCopyNum);
            }
            myDb.CheckOutItem(memberID, Convert.ToInt32(txtISBN.Text), Convert.ToInt16(txtCopyNum.Text));
            
            lblStatus.Text = string.Empty;
            txtISBN.Text = string.Empty;
            txtCopyNum.Text = string.Empty;
            txtCopyNum.ReadOnly = false;
            txtISBN.ReadOnly = false;
            btnCancel.Visible = false;
            btnCheckOut.Visible = true;
            btnFinalCheckOut.Visible = false;

            Session["SessionStatus"] = "Check out successful";
            Response.Redirect("~/MainPanel.aspx");
        }
        catch (SoapException ex)
        {
            if (ex.Code == ExceptionCodes.CheckOutFailed)
            {
                Label lbl = (Label)Page.Master.FindControl("lblStatus");
                lbl.Text = "Check in Failed!";
                lbl.ForeColor = Color.Red;
            }
            else if (ex.Code == ExceptionCodes.ItemNotLoanable)
            {
                StringBuilder myString = new StringBuilder();
                myString.Append("Item is not loanable.");
                Label lbl = (Label)Page.Master.FindControl("lblStatus");
                lbl.Text = myString.ToString(); ;
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
        catch (WebException)
        {
            Label lbl = (Label)Page.Master.FindControl("lblStatus");
            lbl.Text = "Web service is not responding, please try again later.";
            lbl.ForeColor = Color.Red;
        }
    }

    private bool OnLoan(LibraryWebService.Item myItem)
    {
        if (myItem.MemberNumber > 0)
            return true;
        else
            return false;
    }
}
