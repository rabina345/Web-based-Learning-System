using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using SkillCore.Models;

namespace SkillCore.Data_Access_Layer
{
    public static class QuizDAL
    {
        public static List<Quiz> GetByCourseId(int courseId)
        {
            List<Quiz> quizzes = new List<Quiz>();
            using (SqlConnection con = DbHelper.GetConnection())
            {
                SqlCommand cmd = DbHelper.CreateCommand(con, "SELECT * FROM Quizzes WHERE CourseID = @CourseID");
                DbHelper.AddParam(cmd, "@CourseID", courseId);
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        quizzes.Add(MapQuiz(reader));
                    }
                }
            }
            return quizzes;
        }

        public static Quiz GetById(int quizId)
        {
            using (SqlConnection con = DbHelper.GetConnection())
            {
                SqlCommand cmd = DbHelper.CreateCommand(con, "SELECT * FROM Quizzes WHERE QuizID = @QuizID");
                DbHelper.AddParam(cmd, "@QuizID", quizId);
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return MapQuiz(reader);
                    }
                }
            }
            return null;
        }

        private static Quiz MapQuiz(IDataRecord r)
        {
            return new Quiz
            {
                QuizID = DbHelper.GetInt(r, "QuizID"),
                CourseID = DbHelper.GetInt(r, "CourseID"),
                Title = DbHelper.GetString(r, "Title"),
                PassMark = DbHelper.GetInt(r, "PassMark")
            };
        }
    }
}