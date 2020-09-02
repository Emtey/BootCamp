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
//using LibraryBusinessTier;
using Entities;
using System.Drawing;
using System.Web.Services.Protocols;
using ExceptionCodeLibrary;
using System.Net;

using LibraryWebService;

/// <summary>
/// JuvenileMemberPanel
/// </summary>
public partial class JuvenileMemberPanel : System.Web.UI.Page
{
    LibraryWebService.ServiceWse myDb = new LibraryWebService.ServiceWse();
    //DBInteraction myDb = new DBInteraction();
    //JuvenileMember myJuve = new JuvenileMember();
    LibraryWebService.JuvenileMember myJuve = new LibraryWebService.JuvenileMember();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        myDb.SetPolicy("LibraryClientPolicy");
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        DateTime myDate;
        try
        {
            myJuve.FirstName = txtFirstName.Text.ToString();
            myJuve.MiddleInitial = txtMidInit.Text.ToString();
            myJuve.LastName = txtLastName.Text.ToString();
            myJuve.AdultMemberID = Convert.ToInt16(txtAdultMemberID.Text);
            myDate = Convert.ToDateTime(txtBirthDate.Text);
            if (myDate.AddYears(18).Date < DateTime.Today.Date)
            {
                Label lbl = (Label)Page.Master.FindControl("lblStatus");
                lbl.Text = "Person is older than 18.  Add as Adult instead.";
                lbl.ForeColor = Color.Red;
                return;
            }
            else if (myDate.Date > DateTime.Today.Date)
            {
                Label lbl = (Label)Page.Master.FindControl("lblStatus");
                lbl.Text = "Person is not born yet, please wait till they are born to add them.";
                lbl.ForeColor = Color.Red;
                return;
            }
            else
                myJuve.BirthDate = myDate;
                

            myDb.AddJuvenileMember(ref myJuve);

            Session["MemberID"] = myJuve.MemberID;
            Session["SessionStatus"] = "Juvenile Added";
            Response.Redirect("~/MainPanel.aspx");
        }
        catch (SoapException ex)
        {
            if (ex.Code == ExceptionCodes.AddJuvenileFailed)
            {
                Label lbl = (Label)Page.Master.FindControl("lblStatus");
                lbl.Text = "Add Juvenile Failed";
                lbl.ForeColor = Color.Red;
            }
            else 
            {
                Label lbl = (Label)Page.Master.FindControl("lblStatus");
                lbl.Text = "ERROR: Add Juvenile Failed";
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
}
