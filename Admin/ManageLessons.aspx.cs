using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using SkillCore.BLL;
using SkillCore.Models;

namespace SkillCore.Admin
{
    public partial class ManageLessons : System.Web.UI.Page
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
                ddlCourse.DataSource = CourseBLL.GetAll();
                ddlCourse.DataBind();

                BindGrid();
            }
        }

        private void BindGrid()
        {
            Dictionary<int, string> courseTitles;
            List<Lesson> lessons = Data_Access_Layer.LessonDAL.GetAllWithCourseTitle(out courseTitles);

            // Build a small display table combining lesson + course title,
            // since GridView needs a flat list of objects to bind to.
            var displayRows = lessons.Select(l => new
            {
                l.LessonID,
                l.Title,
                l.SortOrder,
                CourseTitle = courseTitles[l.LessonID]
            }).ToList();

            gvLessons.DataSource = displayRows;
            gvLessons.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            try
            {
                int duration;
                int.TryParse(txtDuration.Text, out duration);
                int sortOrder;
                int.TryParse(txtSortOrder.Text, out sortOrder);

                Lesson l = new Lesson
                {
                    LessonID = int.Parse(hdnLessonId.Value),
                    CourseID = int.Parse(ddlCourse.SelectedValue),
                    Title = txtTitle.Text,
                    ContentHtml = txtContent.Text,
                    SortOrder = sortOrder > 0 ? sortOrder : 1,
                    DurationMinutes = duration > 0 ? duration : (int?)null
                };

                LessonBLL.Save(l);

                txtTitle.Text = "";
                txtContent.Text = "";
                txtSortOrder.Text = "1";
                txtDuration.Text = "";
                hdnLessonId.Value = "0";
                litFormHeading.Text = "Add Lesson";
                litMessage.Text = "<p style='color:green;'>Saved.</p>";

                BindGrid();
            }
            catch (ArgumentException ex)
            {
                litMessage.Text = "<p style='color:red;'>" + ex.Message + "</p>";
            }
        }

        protected void gvLessons_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            int lessonId = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "DeleteLesson")
            {
                LessonBLL.Delete(lessonId);
                BindGrid();
            }
            else if (e.CommandName == "EditLesson")
            {
                Lesson l = LessonBLL.GetById(lessonId);
                hdnLessonId.Value = l.LessonID.ToString();
                ddlCourse.SelectedValue = l.CourseID.ToString();
                txtTitle.Text = l.Title;
                txtContent.Text = l.ContentHtml;
                txtSortOrder.Text = l.SortOrder.ToString();
                txtDuration.Text = l.DurationMinutes?.ToString();
                litFormHeading.Text = "Edit Lesson";
            }
        }
    }
}