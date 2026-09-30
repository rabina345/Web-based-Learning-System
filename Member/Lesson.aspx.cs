using System;
using SkillCore.BLL;
using SkillCore.Models;

namespace SkillCore.Member
{
    public partial class Lesson : System.Web.UI.Page
    {
        protected int courseIdForBackLink;
        private int lessonId;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("~/Account/Login.aspx");
                return;
            }

            if (!int.TryParse(Request.QueryString["id"], out lessonId))
            {
                Response.Redirect("~/Pages/Courses.aspx");
                return;
            }

            Models.Lesson lessonModel = LessonBLL.GetById(lessonId);
            if (lessonModel == null)
            {
                Response.Redirect("~/Pages/Courses.aspx");
                return;
            }

            courseIdForBackLink = lessonModel.CourseID;

            // Only enrolled students can view lesson content
            int userId = (int)Session["UserID"];
            if (!EnrollmentBLL.IsEnrolled(userId, lessonModel.CourseID))
            {
                Response.Redirect("~/Pages/CourseDetails.aspx?id=" + lessonModel.CourseID);
                return;
            }

            litTitle.Text = lessonModel.Title;
            litContent.Text = lessonModel.ContentHtml;

            if (LessonProgressBLL.IsCompleted(userId, lessonId))
            {
                litStatus.Text = "<p style='color:green;'>You've completed this lesson.</p>";
                btnComplete.Visible = false;
            }
        }

        protected void btnComplete_Click(object sender, EventArgs e)
        {
            LessonProgressBLL.MarkComplete((int)Session["UserID"], lessonId);
            Response.Redirect(Request.RawUrl);
        }
    }
}