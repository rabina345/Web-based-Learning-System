using System;
using SkillCore.BLL;

namespace SkillCore.Pages
{
    public partial class Contact : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // If someone's logged in, pre-fill their name/email so they
            // don't have to retype it
            if (!IsPostBack && Session["UserID"] != null)
            {
                txtName.Text = Session["FullName"].ToString();
            }
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            try
            {
                int? userId = Session["UserID"] != null ? (int?)Session["UserID"] : null;

                FeedbackBLL.Submit(txtName.Text, txtEmail.Text, txtSubject.Text, txtMessage.Text, userId);

                pnlForm.Visible = false;
                litMessage.Text = "<p style='color:green;'>Thanks! Your message has been sent.</p>";
            }
            catch (ArgumentException ex)
            {
                litMessage.Text = "<p style='color:red;'>" + ex.Message + "</p>";
            }
        }
    }
}