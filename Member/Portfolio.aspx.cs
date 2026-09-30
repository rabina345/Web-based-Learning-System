using SkillCore.BLL;
using SkillCore.Models;
using System;
using System.IO;
using System.Web.UI.WebControls;

namespace SkillCore.Member
{
    public partial class Portfolio : System.Web.UI.Page
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
            rptPortfolio.DataSource = PortfolioBLL.GetByUserId((int)Session["UserID"]);
            rptPortfolio.DataBind();
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            if (!fuFile.HasFile)
            {
                litMessage.Text = "<p style='color:red;'>Please choose a file.</p>";
                return;
            }

            try
            {
                // Save the physical file into ~/Uploads/Portfolios/, with a
                // unique filename so two students uploading "resume.pdf"
                // don't overwrite each other
                string uploadsFolder = Server.MapPath("~/Uploads/Portfolios/");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                string uniqueFileName = Guid.NewGuid().ToString("N") + "_" + Path.GetFileName(fuFile.FileName);
                string physicalPath = Path.Combine(uploadsFolder, uniqueFileName);
                fuFile.SaveAs(physicalPath);

                string webPath = ResolveUrl("~/Uploads/Portfolios/" + uniqueFileName);

                Models.Portfolio p = new Models.Portfolio
                {
                    UserID = (int)Session["UserID"],
                    Title = txtTitle.Text,
                    Description = txtDescription.Text,
                    FilePath = webPath
                };

                PortfolioBLL.Save(p);

                txtTitle.Text = "";
                txtDescription.Text = "";
                litMessage.Text = "<p style='color:green;'>Uploaded successfully.</p>";

                BindGrid();
            }
            catch (ArgumentException ex)
            {
                litMessage.Text = "<p style='color:red;'>" + ex.Message + "</p>";
            }
        }

        protected void lnkDelete_Command(object sender, CommandEventArgs e)
        {
            int portfolioId = Convert.ToInt32(e.CommandArgument);

            try
            {
                PortfolioBLL.Delete(portfolioId, (int)Session["UserID"], (int)Session["RoleID"] == 1);
                BindGrid();
            }
            catch (ArgumentException ex)
            {
                litMessage.Text = "<p style='color:red;'>" + ex.Message + "</p>";
            }
        }
    }
}