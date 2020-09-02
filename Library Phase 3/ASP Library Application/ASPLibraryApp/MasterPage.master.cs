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
using System.Drawing;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "Library Application";
        if (Session["SessionStatus"] != null)
        {
            lblStatus.Text = Session["SessionStatus"].ToString();
            lblStatus.ForeColor = Color.Blue;
            Session["SessionStatus"] = null;
        }
    }
}
