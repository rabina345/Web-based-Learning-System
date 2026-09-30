using System;
using SkillCore.BLL;
using SkillCore.Models;

namespace SkillCore.Account
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            User user = UserBLL.Login(txtEmail.Text, txtPassword.Text);

            if (user == null)
            {
                litMessage.Text = "<p style='color:red;'>Invalid email or password.</p>";
                return;
            }

            // Store who's logged in for the rest of the session.
            Session["UserID"] = user.UserID;
            Session["FullName"] = user.FullName;
            Session["RoleID"] = user.RoleID;

            if (user.RoleID == 1)
                Response.Redirect("~/Admin/Dashboard.aspx");
            else
                Response.Redirect("~/Default.aspx");
        }

    }
}