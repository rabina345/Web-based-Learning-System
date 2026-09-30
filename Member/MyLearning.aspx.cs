using System;
using SkillCore.BLL;

namespace SkillCore.Member
{
    public partial class MyLearning : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("~/Account/Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                var courses = EnrollmentBLL.GetEnrolledCourses((int)Session["UserID"]);

                if (courses.Count == 0)
                {
                    litEmpty.Visible = true;
                }
                else
                {
                    rptEnrolled.DataSource = courses;
                    rptEnrolled.DataBind();
                }
            }
        }
    }
}