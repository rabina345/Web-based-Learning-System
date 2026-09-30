using System;
using SkillCore.BLL;

namespace SkillCore.Pages
{
    public partial class Courses : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                rptCourses.DataSource = CourseBLL.GetAllPublished();
                rptCourses.DataBind();
            }
        }
    }
}