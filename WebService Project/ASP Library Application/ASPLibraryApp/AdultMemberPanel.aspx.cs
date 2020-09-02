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
using System.IO;
using System.Web.Services.Protocols;
using ExceptionCodeLibrary;
using System.Net;

using LibraryWebService;

/// <summary>
/// AdultMemberPanel
/// </summary>
public partial class AdultMemberPanel : System.Web.UI.Page
{
    //DBInteraction myDb = new DBInteraction();
    LibraryWebService.ServiceWse myDb = new LibraryWebService.ServiceWse();
    //AdultMember myAdult = new AdultMember();
    LibraryWebService.AdultMember myAdult = new LibraryWebService.AdultMember();

    protected void Page_Load(object sender, EventArgs e)
    {
        myDb.SetPolicy("LibraryClientPolicy");
        if (!IsPostBack)
        {
            DataSet dsStates = (DataSet)Application["States"];

            ddState.DataTextField = "Abbreviation";
            ddState.DataValueField = "Abbreviation";

            ddState.DataSource = dsStates;
            ddState.DataBind();
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            myAdult.FirstName = txtFirstName.Text.ToString();
            myAdult.MiddleInitial = txtMidInit.Text.ToString();
            myAdult.LastName = txtLastName.Text.ToString();
            myAdult.Street = txtAddress.Text.ToString();
            myAdult.City = txtCity.Text.ToString();
            myAdult.State = ddState.Text.ToString();
            myAdult.ZipCode = txtZip.Text.ToString();
            myAdult.PhoneNumber = txtTelephone.Text.ToString();

            //myDb.AddMember(myAdult);
            myDb.AddAdultMember(ref myAdult);
            
            Session["MemberID"] = myAdult.MemberID;
            Session["SessionStatus"] = "Adult Added";
            Response.Redirect("~/MainPanel.aspx");
        }
        catch (SoapException ex)
        {
            if (ex.Code == ExceptionCodes.AddAdultFailed)
            {
                Label lbl = (Label)Page.Master.FindControl("lblStatus");
                lbl.Text = "Add Adult Failed";
                lbl.ForeColor = Color.Red;
            }
            if (ex.Code == ExceptionCodes.GenericException)
            {
                Label lbl = (Label)Page.Master.FindControl("lblStatus");
                lbl.Text = "ERROR: " + ex.Message;
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
