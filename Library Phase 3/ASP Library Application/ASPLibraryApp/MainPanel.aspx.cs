using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Entities;
using LibraryBusinessTier;
using LibraryDataAccess;
using System.Drawing;
using System.Text;

/// <summary>
/// MainPanel
/// </summary>
public partial class MainPanel : System.Web.UI.Page
{
    DBInteraction myDb = new DBInteraction();
    Member myMember = new Member();
    JuvenileMember juve = new JuvenileMember();

    bool isExpired = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["MemberID"] != null && Convert.ToInt16(Session["MemberID"]) > 0)
            {
                myMember = (Member)myDb.GetMember(Convert.ToInt16(Session["MemberID"]));
                LoadGenericFields(myMember);
                if (myMember is JuvenileMember)
                    LoadJuvenileFields(myMember);
                else
                    FormatAdultWindow();
            }
        }
        else
            Session["SessionStatus"] = null;
    }

    #region GetInfo Button Click
    protected void btnGetInfo_Click(object sender, EventArgs e)
    {
        string myErr;
        try
        {
            myMember = (Member)myDb.GetMember(Convert.ToInt16(txtMemberID.Text));
            LoadGenericFields(myMember);
            if (myMember is JuvenileMember)
                LoadJuvenileFields(myMember);
            else
                FormatAdultWindow();
            Session["MemberID"] = myMember.MemberID.ToString();

            CheckExpirationDate(myMember);

            if (!isExpired)
            {
                if (GridView1.Rows.Count < 4)
                    btnCheckout.Visible = true;
                else
                    btnCheckout.Visible = false;
            }

            ConvertToAdult(myMember);
            //load the datagrid view.
            ObjectDataSource1.DataBind();
        }
        catch (LibraryException)
        {
            myErr = string.Format("Member ID: {0} was not found.", txtMemberID.Text);
            Label lbl = (Label)Page.Master.FindControl("lblStatus");
            lbl.Text = myErr;
            lbl.ForeColor = Color.Red;
            txtMemberID.Focus();
        }
    }
    #endregion

    #region CheckExpirationDate
    private void CheckExpirationDate(Member myMember)
    {
        string myErr;
        //check member Expireation date
        if ((DateTime)myMember.ExpirationDate.Date < DateTime.Now.Date)
        {
            txtExpDte.BackColor = Color.Red;
            if (myMember is JuvenileMember)
            {
                btnRenewMember.Visible = false;
                myErr = string.Format("Member ID: {0} is expired. Renew Adult Member.", txtMemberID.Text);
            }
            else
            {
                btnRenewMember.Visible = true;
                myErr = string.Format("Member ID: {0} is expired.", txtMemberID.Text);
            }
            btnCheckout.Visible = false;
            Label lbl = (Label)Page.Master.FindControl("lblStatus");
            lbl.Text = myErr;
            lbl.ForeColor = Color.Red;
            isExpired = true;
        }
        else if ((DateTime)myMember.ExpirationDate.Date < DateTime.Now.Date.AddDays(14) &&
                    (DateTime)myMember.ExpirationDate.Date == DateTime.Now.Date)
        {
            txtExpDte.BackColor = Color.Yellow;
            if (myMember is JuvenileMember)
            {
                btnRenewMember.Visible = false;
                myErr = string.Format("Member ID: {0} is set to expire in 14 days. Renew Adult Member", txtMemberID.Text);
            }
            else
            {
                btnRenewMember.Visible = true;
                myErr = string.Format("Member ID: {0} is set to expire in 14 days.", txtMemberID.Text);
            }
            myErr = string.Format("Member ID: {0} is set to expire in 14 days.", txtMemberID.Text);
            Label lbl = (Label)Page.Master.FindControl("lblStatus");
            lbl.Text = myErr;
            lbl.ForeColor = Color.Yellow;
            isExpired = false;
        }
        else
        {
            txtExpDte.BackColor = Color.White;
            btnRenewMember.Visible = false;
        }
    }
    #endregion

    #region ConvertToAdult
    private void ConvertToAdult(Member myMember)
    {
        string myErr;
        if (myMember is JuvenileMember)
        {
            JuvenileMember myJuv = (JuvenileMember)myMember;
            if ((DateTime)myJuv.BirthDate.AddYears(+18) <= DateTime.Today)
            {
                try
                {
                    myDb.ConvertJuvenile(myJuv.MemberID);
                    myErr = string.Format("Member ID: {0} Converted to Adult.", txtMemberID.Text);
                    myMember = (Member)myDb.GetMember(Convert.ToInt16(txtMemberID.Text));

                    LoadGenericFields(myMember);
                    FormatAdultWindow();

                    Label lbl = (Label)Page.Master.FindControl("lblStatus");
                    lbl.Text = myErr;
                    lbl.ForeColor = Color.Blue;
                    txtExpDte.BackColor = Color.White;
                    btnRenewMember.Visible = false;
                }
                catch (LibraryException)
                {
                    myErr = string.Format("Member ID: {0} could not be converted from Juvenile to Adult.", txtMemberID.Text);
                    Label lbl = (Label)Page.Master.FindControl("lblStatus");
                    lbl.Text = myErr;
                }
            }
        }
    }
    #endregion

    #region Field Load helper methods
    //Loads the common fields Juvenile and Adult members share.
    private void LoadGenericFields(Member myMember)
    {
        txtMemberID.Text = myMember.MemberID.ToString();
        txtExpDte.Text = myMember.ExpirationDate.ToString("MM/dd/yyyy");
        txtFirstName.Text = myMember.FirstName;
        txtLastName.Text = myMember.LastName;
        txtMiddleInitial.Text = myMember.MiddleInitial;
        txtAddress.Text = myMember.Street;
        txtCity.Text = myMember.City;
        txtState.Text = myMember.State;
        txtZip.Text = myMember.ZipCode;
        txtTelephone.Text = myMember.PhoneNumber;
    }
    //loads the fields that are common to Juvenile members.
    private void LoadJuvenileFields(Member myMember)
    {
        juve = (JuvenileMember)myMember;

        //set the fields visibles
        lblBirthDte.Visible = true;
        lblAdultMember.Visible = true;
        txtBirthDte.Visible = true;
        txtAdultMemberID.Visible = true;

        //move the myMemberfields into the txt fields.            
        txtAdultMemberID.Text = juve.AdultMemberID.ToString();
        txtBirthDte.Text = juve.BirthDate.ToString("MM/dd/yyyy");
    }
    //Hides the Juvenile fields that we dont need to see for adults.
    private void FormatAdultWindow()
    {
        //set the fields visibles to false if they were true
        lblBirthDte.Visible = false;
        lblAdultMember.Visible = false;
        txtBirthDte.Visible = false;
        txtAdultMemberID.Visible = false;
    }
    #endregion

    #region RenewMember
    protected void btnRenewMember_Click(object sender, EventArgs e)
    {
        try
        {
            myDb.RenewMember(Convert.ToInt16(txtMemberID.Text));
            btnGetInfo_Click(sender, e);
        }
        catch (LibraryException)
        {
            string myErr = string.Format("Member ID: {0} could not be renewed.", txtMemberID.Text);
            Label lbl = (Label)Page.Master.FindControl("lblStatus");
            lbl.Text = myErr;
            lbl.ForeColor = Color.Red;
            txtMemberID.Focus();
        }
    }
    #endregion

    #region DataGrid Stuff
    protected void ObjectDataSource1_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        if (txtMemberID.Text.Trim().Length == 0)
        {
            e.Cancel = true;
        }
    }

    protected void ObjectDataSource1_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (!isExpired)
        {
            if (GridView1.Rows.Count < 4)
                btnCheckout.Visible = true;
            else
                btnCheckout.Visible = false;
        }
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            int myIsbn = Convert.ToInt32(GridView1.Rows[GridView1.SelectedIndex].Cells[1].Text);
            short myCopyNum = Convert.ToInt16(GridView1.Rows[GridView1.SelectedIndex].Cells[2].Text);

            myDb.CheckInItem(myIsbn, myCopyNum);

            ObjectDataSource1.DataBind();
            GridView1.DataBind();
            GridView1.SelectedIndex = -1;
        }
        catch (LibraryException ex)
        {
            StringBuilder myString = new StringBuilder();
            myString.Append("An error was encountered on check in. ");
            myString.Append(ex.Message);
            Label lbl = (Label)Page.Master.FindControl("lblStatus");
            lbl.Text = myString.ToString();
            lbl.ForeColor = Color.Red;
        }
    }
    
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        GridView myGrid = (GridView)sender;
        for (int i = 0; i < myGrid.Rows.Count; i++)
        {
            if (Convert.ToDateTime(((Label)myGrid.Rows[i].Cells[6].Controls[1]).Text).Date < DateTime.Today.Date)
            {
                myGrid.Rows[i].BackColor = Color.Tomato;
            }
        }

        if (myGrid.Rows.Count < 4)
            btnCheckout.Visible = true;
        else
            btnCheckout.Visible = false;
    }
    #endregion
}
