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
using System.Text;

/// <summary>
/// ItemPanel
/// </summary>
public partial class ItemPanel : System.Web.UI.Page
{
    DBInteraction myDb = new DBInteraction(); 

    protected void Page_Load(object sender, EventArgs e)
    {  
        //string Script = "return confirm('Are you sure you wish to add this item?')";
        //ClientScript.RegisterOnSubmitStatement(this.GetType(), "ConfirmAdd", Script);
    }
    protected void btnAddItem_Click(object sender, EventArgs e)
    {
        try
        {
            int isbn = Convert.ToInt32(txtISBN.Text);
            string title = txtTitle.Text;
            string author = txtAuthor.Text;
            string loanable = "";
            if (chkLoanable.Checked)
               loanable = "Y";
            else
               loanable = "N";

           myDb.AddItem(isbn, title, author, loanable);
           Label lbl = (Label)Page.Master.FindControl("lblStatus");
           lbl.Text = "Item Added";
           lbl.ForeColor = Color.Blue;
        }
        catch (LibraryException ex)
        {
            if (ex.LibraryErrorCode == ErrorCode.AddItemFailed)
            {
                StringBuilder myString = new StringBuilder();
                myString.Append("Add Item Failed");
                Label lbl = (Label)Page.Master.FindControl("lblStatus");
                lbl.Text = myString.ToString();
                lbl.ForeColor = Color.Red;
            }
        }
    }
}
