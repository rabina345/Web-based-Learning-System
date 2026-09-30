using System;

namespace Skill_Core
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] != null)
            {
                phGuest.Visible = false;
                phLoggedIn.Visible = true;

                litGreeting.Visible = true;
                litGreeting.Text = "<h1>Hi, " + Session["FullName"] + "</h1>";

                if ((int)Session["RoleID"] == 1)
                {
                    phAdminLink.Visible = true;
                }
            }
        }

        protected void lnkLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("~/Default.aspx");
        }
    }
}