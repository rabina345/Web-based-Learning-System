using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using SkillCore.BLL;

namespace SkillCore.Member
{
    public partial class AllPortfolios : System.Web.UI.Page
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
                BindGrid();
            }
        }

        private void BindGrid()
        {
            Dictionary<int, string> uploaderNames;
            var portfolios = PortfolioBLL.GetAll(out uploaderNames);

            var displayRows = portfolios.Select(p => new
            {
                p.PortfolioID,
                p.UserID,
                p.Title,
                p.Description,
                p.FilePath,
                UploaderName = uploaderNames[p.PortfolioID]
            }).ToList();

            rptPortfolios.DataSource = displayRows;
            rptPortfolios.DataBind();
        }

        // Called from the markup to decide whether to show the Delete link
        // for each row — true if it's your own upload, or you're Admin
        protected bool CanDelete(int uploaderUserId)
        {
            int currentUserId = (int)Session["UserID"];
            bool isAdmin = (int)Session["RoleID"] == 1;
            return uploaderUserId == currentUserId || isAdmin;
        }

        protected void lnkDelete_Command(object sender, CommandEventArgs e)
        {
            int portfolioId = Convert.ToInt32(e.CommandArgument);
            bool isAdmin = (int)Session["RoleID"] == 1;

            try
            {
                PortfolioBLL.Delete(portfolioId, (int)Session["UserID"], isAdmin);
                BindGrid();
            }
            catch (ArgumentException ex)
            {
                litMessage.Text = "<p style='color:red;'>" + ex.Message + "</p>";
            }
        }
    }
}