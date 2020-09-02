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
using LibraryBusinessTier;
using Entities;
using System.Drawing;

/// <summary>
/// JuvenileMemberPanel
/// </summary>
public partial class JuvenileMemberPanel : System.Web.UI.Page
{
    DBInteraction myDb = new DBInteraction();
    JuvenileMember myJuve = new JuvenileMember();
    
    protected void Page_Load(object sender, EventArgs e)
    {

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
                

            myDb.AddMember(myJuve);

            Session["MemberID"] = myJuve.MemberID;
            Session["SessionStatus"] = "Juvenile Added";
            Response.Redirect("~/MainPanel.aspx");
        }
        catch (LibraryException ex)
        {
            if (ex.LibraryErrorCode == ErrorCode.AddJuvenileFailed)
            {
                Label lbl = (Label)Page.Master.FindControl("lblStatus");
                lbl.Text = "Add Juvenile Failed";
                lbl.ForeColor = Color.Red;
            }
            if (ex.LibraryErrorCode == ErrorCode.GenericException)
            {
                Label lbl = (Label)Page.Master.FindControl("lblStatus");
                lbl.Text = "ERROR: " + ex.Message;
                lbl.ForeColor = Color.Red;
            }
        }
    }
}
