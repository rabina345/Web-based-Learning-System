using System;
using System.Web.UI.WebControls;
using SkillCore.BLL;
using SkillCore.Models;

namespace SkillCore.Admin
{
    public partial class ManageUsers : System.Web.UI.Page
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
            gvUsers.DataSource = UserBLL.GetAll();
            gvUsers.DataBind();
        }

        // Sets each row's dropdown/checkbox to match that user's current
        // RoleID/IsActive, since GridView can't bind those automatically
        protected void gvUsers_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow) return;

            User u = (User)e.Row.DataItem;
            DropDownList ddlRole = (DropDownList)e.Row.FindControl("ddlRole");
            CheckBox chkActive = (CheckBox)e.Row.FindControl("chkActive");

            ddlRole.SelectedValue = u.RoleID.ToString();
            chkActive.Checked = u.IsActive;
        }

        protected void gvUsers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int userId = Convert.ToInt32(e.CommandArgument);
            int currentAdminId = (int)Session["UserID"];

            try
            {
                if (e.CommandName == "DeleteUser")
                {
                    UserBLL.Delete(userId, currentAdminId);
                    litMessage.Text = "<p style='color:green;'>User deleted.</p>";
                }
                else if (e.CommandName == "UpdateUser")
                {
                    GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                    DropDownList ddlRole = (DropDownList)row.FindControl("ddlRole");
                    CheckBox chkActive = (CheckBox)row.FindControl("chkActive");

                    UserBLL.UpdateRoleAndStatus(userId, int.Parse(ddlRole.SelectedValue), chkActive.Checked, currentAdminId);
                    litMessage.Text = "<p style='color:green;'>User updated.</p>";
                }

                BindGrid();
            }
            catch (ArgumentException ex)
            {
                litMessage.Text = "<p style='color:red;'>" + ex.Message + "</p>";
            }
        }
    }
}