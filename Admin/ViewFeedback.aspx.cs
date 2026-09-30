using System;
using System.Web.UI.WebControls;
using SkillCore.BLL;
using SkillCore.Models;

namespace SkillCore.Admin
{
    public partial class ViewFeedback : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["RoleID"] == null || (int)Session["RoleID"] != 1)
            {
                Response.Redirect("~/Account/Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                BindGrid();
            }
        }

        private void BindGrid()
        {
            gvFeedback.DataSource = FeedbackBLL.GetAll();
            gvFeedback.DataBind();
        }

        // Hides the "Mark as Read" link for messages already marked read
        protected void gvFeedback_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow) return;

            Feedback f = (Feedback)e.Row.DataItem;
            if (f.IsRead)
            {
                LinkButton lnkMarkRead = (LinkButton)e.Row.FindControl("lnkMarkRead");
                lnkMarkRead.Visible = false;
            }
        }

        protected void gvFeedback_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int feedbackId = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "DeleteFeedback")
            {
                FeedbackBLL.Delete(feedbackId);
            }
            else if (e.CommandName == "MarkRead")
            {
                FeedbackBLL.MarkAsRead(feedbackId);
            }

            BindGrid();
        }
    }
}