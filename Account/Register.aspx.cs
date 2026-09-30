using System;
using SkillCore.BLL;

namespace SkillCore.Account
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            try
            {
                UserBLL.Register(txtFullName.Text, txtEmail.Text, txtPassword.Text);
                litMessage.Text = "<p style='color:green;'>Account created! You can now log in.</p>";

                txtFullName.Text = "";
                txtEmail.Text = "";
                txtPassword.Text = "";
            }
            catch (ArgumentException ex)
            {
                litMessage.Text = "<p style='color:red;'>" + ex.Message + "</p>";
            }
        }
    }
}