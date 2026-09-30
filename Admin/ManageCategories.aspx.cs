using System;
using SkillCore.BLL;
using SkillCore.Models;

namespace SkillCore.Admin
{
    public partial class ManageCategories : System.Web.UI.Page
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
            gvCategories.DataSource = CategoryBLL.GetAll();
            gvCategories.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            try
            {
                Category c = new Category
                {
                    CategoryID = int.Parse(hdnCategoryId.Value),
                    CategoryName = txtName.Text,
                    Description = txtDescription.Text
                };

                CategoryBLL.Save(c);

                txtName.Text = "";
                txtDescription.Text = "";
                hdnCategoryId.Value = "0";
                litFormHeading.Text = "Add Category";
                litMessage.Text = "<p style='color:green;'>Saved.</p>";

                BindGrid();
            }
            catch (ArgumentException ex)
            {
                litMessage.Text = "<p style='color:red;'>" + ex.Message + "</p>";
            }
        }

        protected void gvCategories_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            int categoryId = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "DeleteCategory")
            {
                CategoryBLL.Delete(categoryId);
                BindGrid();
            }
            else if (e.CommandName == "EditCategory")
            {
                Category c = CategoryBLL.GetById(categoryId);
                hdnCategoryId.Value = c.CategoryID.ToString();
                txtName.Text = c.CategoryName;
                txtDescription.Text = c.Description;
                litFormHeading.Text = "Edit Category";
            }
        }
    }
}