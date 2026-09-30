using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using SkillCore.BLL;
using SkillCore.Models;

namespace SkillCore.Member
{
    public partial class Quiz : System.Web.UI.Page
    {
        private int quizId;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("~/Account/Login.aspx");
                return;
            }

            if (!int.TryParse(Request.QueryString["id"], out quizId))
            {
                Response.Redirect("~/Pages/Courses.aspx");
                return;
            }

            if (!IsPostBack)
            {
                SkillCore.Models.Quiz quiz = QuizBLL.GetById(quizId);
                if (quiz == null)
                {
                    Response.Redirect("~/Pages/Courses.aspx");
                    return;
                }

                litQuizTitle.Text = quiz.Title;

                rptQuestions.DataSource = QuizBLL.GetQuestions(quizId);
                rptQuestions.DataBind();
            }
        }

        protected void rptQuestions_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem)
                return;

            Question q = (Question)e.Item.DataItem;
            RadioButtonList rbl = (RadioButtonList)e.Item.FindControl("rblOptions");

            rbl.DataSource = QuizBLL.GetOptions(q.QuestionID);
            rbl.DataBind();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Dictionary<int, int> answers = new Dictionary<int, int>();

            foreach (RepeaterItem item in rptQuestions.Items)
            {
                HiddenField hdnQuestionId = (HiddenField)item.FindControl("hdnQuestionId");
                RadioButtonList rbl = (RadioButtonList)item.FindControl("rblOptions");

                if (rbl.SelectedItem != null)
                {
                    int questionId = int.Parse(hdnQuestionId.Value);
                    int optionId = int.Parse(rbl.SelectedValue);
                    answers[questionId] = optionId;
                }
            }

            var result = QuizBLL.SubmitAttempt((int)Session["UserID"], quizId, answers);

            rptQuestions.Visible = false;
            btnSubmit.Visible = false;
            pnlResult.Visible = true;

            string color = result.isPassed ? "green" : "red";
            string verdict = result.isPassed ? "Passed!" : "Not passed — try again.";
            litResult.Text = $"<p style='color:{color};'>Score: {result.score}/{result.totalMarks} — {verdict}</p>";
        }
    }
}