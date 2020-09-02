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

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Login1.Attributes.Add("align", "center");
        LoginStatus ls = (LoginStatus)Page.Master.FindControl("LoginStatus1");
        ls.Visible = false;

        TreeView tv = (TreeView)Page.Master.FindControl("TreeView1");
        tv.Visible = false;
    }

    private void Page_Error(object sender, EventArgs e)
    {
        Response.Write(Server.GetLastError().Message);
        Server.ClearError();
    }

}
