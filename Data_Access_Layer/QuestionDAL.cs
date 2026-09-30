using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using SkillCore.Models;

namespace SkillCore.Data_Access_Layer
{
    public static class QuestionDAL
    {
        public static List<Question> GetByQuizId(int quizId)
        {
            List<Question> questions = new List<Question>();
            using (SqlConnection con = DbHelper.GetConnection())
            {
                SqlCommand cmd = DbHelper.CreateCommand(con, "SELECT * FROM Questions WHERE QuizID = @QuizID");
                DbHelper.AddParam(cmd, "@QuizID", quizId);
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        questions.Add(new Question
                        {
                            QuestionID = DbHelper.GetInt(reader, "QuestionID"),
                            QuizID = DbHelper.GetInt(reader, "QuizID"),
                            QuestionText = DbHelper.GetString(reader, "QuestionText"),
                            Marks = DbHelper.GetInt(reader, "Marks")
                        });
                    }
                }
            }
            return questions;
        }

        public static List<QuestionOption> GetOptionsByQuestionId(int questionId)
        {
            List<QuestionOption> options = new List<QuestionOption>();
            using (SqlConnection con = DbHelper.GetConnection())
            {
                SqlCommand cmd = DbHelper.CreateCommand(con, "SELECT * FROM QuestionOptions WHERE QuestionID = @QuestionID");
                DbHelper.AddParam(cmd, "@QuestionID", questionId);
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        options.Add(new QuestionOption
                        {
                            OptionID = DbHelper.GetInt(reader, "OptionID"),
                            QuestionID = DbHelper.GetInt(reader, "QuestionID"),
                            OptionText = DbHelper.GetString(reader, "OptionText"),
                            IsCorrect = DbHelper.GetBool(reader, "IsCorrect")
                        });
                    }
                }
            }
            return options;
        }

        // Used only for scoring — never expose this to the client-facing page
        public static bool IsOptionCorrect(int optionId)
        {
            using (SqlConnection con = DbHelper.GetConnection())
            {
                SqlCommand cmd = DbHelper.CreateCommand(con, "SELECT IsCorrect FROM QuestionOptions WHERE OptionID = @OptionID");
                DbHelper.AddParam(cmd, "@OptionID", optionId);
                con.Open();
                object result = cmd.ExecuteScalar();
                return result != null && (bool)result;
            }
        }
    }
}