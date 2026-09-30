using SkillCore.BLL;
using System;
using System.Web.UI.WebControls;

namespace SkillCore.Admin
{
    public partial class ManageCourses : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Block anyone who isn't logged in as Admin (RoleID 1)
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
            gvCourses.DataSource = CourseBLL.GetAll();
            gvCourses.DataBind();
        }

        protected void gvCourses_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteCourse")
            {
                int courseId = Convert.ToInt32(e.CommandArgument);
                CourseBLL.Delete(courseId);
                BindGrid();
            }
        }
    }
}