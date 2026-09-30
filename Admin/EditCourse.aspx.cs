using System;
using SkillCore.BLL;
using SkillCore.Models;

namespace SkillCore.Admin
{
    public partial class EditCourse : System.Web.UI.Page
    {
        private int courseId = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["RoleID"] == null || (int)Session["RoleID"] != 1)
            {
                Response.Redirect("~/Account/Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                ddlCategory.DataSource = CategoryBLL.GetAll();
                ddlCategory.DataBind();

                if (int.TryParse(Request.QueryString["id"], out courseId) && courseId > 0)
                {
                    litHeading.Text = "Edit Course";
                    LoadCourse(courseId);
                }
            }
            else
            {
                int.TryParse(Request.QueryString["id"], out courseId);
            }
        }

        private void LoadCourse(int id)
        {
            Course c = CourseBLL.GetById(id);
            if (c == null)
            {
                Response.Redirect("~/Admin/ManageCourses.aspx");
                return;
            }

            txtTitle.Text = c.Title;
            txtShortDescription.Text = c.ShortDescription;
            txtFullDescription.Text = c.FullDescription;
            ddlCategory.SelectedValue = c.CategoryID.ToString();
            ddlDifficulty.SelectedValue = c.DifficultyLevel;
            txtDuration.Text = c.DurationMinutes?.ToString();
            chkPublished.Checked = c.IsPublished;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            try
            {
                int duration;
                int.TryParse(txtDuration.Text, out duration);

                Course c = new Course
                {
                    CourseID = courseId,
                    Title = txtTitle.Text,
                    ShortDescription = txtShortDescription.Text,
                    FullDescription = txtFullDescription.Text,
                    CategoryID = int.Parse(ddlCategory.SelectedValue),
                    DifficultyLevel = ddlDifficulty.SelectedValue,
                    DurationMinutes = duration > 0 ? duration : (int?)null,
                    IsPublished = chkPublished.Checked,
                    CreatedBy = (int)Session["UserID"]
                };

                CourseBLL.Save(c);
                Response.Redirect("~/Admin/ManageCourses.aspx");
            }
            catch (ArgumentException ex)
            {
                litMessage.Text = "<p style='color:red;'>" + ex.Message + "</p>";
            }
        }
    }
}