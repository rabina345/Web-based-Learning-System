using System;
using SkillCore.BLL;
using SkillCore.Models;

namespace SkillCore.Pages
{
    public partial class CourseDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int courseId;
            if (!int.TryParse(Request.QueryString["id"], out courseId))
            {
                Response.Redirect("~/Pages/Courses.aspx");
                return;
            }

            Course course = CourseBLL.GetById(courseId);
            if (course == null)
            {
                Response.Redirect("~/Pages/Courses.aspx");
                return;
            }

            if (Session["UserID"] == null)
            {
                btnEnroll.Text = "Log in to enroll";
                btnEnroll.Enabled = false;
            }
            else if (EnrollmentBLL.IsEnrolled((int)Session["UserID"], courseId))
            {
                btnEnroll.Text = "Already Enrolled";
                btnEnroll.Enabled = false;
            }

            litTitle.Text = course.Title;
            litMeta.Text = course.CategoryName + " · " + course.DifficultyLevel;
            litDescription.Text = course.FullDescription ?? course.ShortDescription;

            rptLessons.DataSource = LessonBLL.GetByCourseId(courseId);
            rptLessons.DataBind();

            var quizzes = QuizBLL.GetByCourseId(courseId);
            if (quizzes.Count > 0)
            {
                litQuizLink.Text = "<p><a href='" + ResolveUrl("~/Member/Quiz.aspx") + "?id=" + quizzes[0].QuizID + "'>Take the quiz for this course</a></p>";
            }


        }



        protected void btnEnroll_Click(object sender, EventArgs e)
        {
            int courseId;
            int.TryParse(Request.QueryString["id"], out courseId);

            try
            {
                EnrollmentBLL.Enroll((int)Session["UserID"], courseId);
                Response.Redirect(Request.RawUrl); // reload the page so the button updates
            }
            catch (ArgumentException ex)
            {
                litEnrollMessage.Text = "<p style='color:red;'>" + ex.Message + "</p>";
            }
        }
    }
}