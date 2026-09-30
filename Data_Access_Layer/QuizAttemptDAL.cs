using System.Data.SqlClient;

namespace SkillCore.Data_Access_Layer
{
    public static class QuizAttemptDAL
    {
        public static void Insert(int userId, int quizId, int score, int totalMarks, bool isPassed)
        {
            using (SqlConnection con = DbHelper.GetConnection())
            {
                string sql = @"INSERT INTO QuizAttempts (UserID, QuizID, Score, TotalMarks, IsPassed)
                                VALUES (@UserID, @QuizID, @Score, @TotalMarks, @IsPassed)";
                SqlCommand cmd = DbHelper.CreateCommand(con, sql);
                DbHelper.AddParam(cmd, "@UserID", userId);
                DbHelper.AddParam(cmd, "@QuizID", quizId);
                DbHelper.AddParam(cmd, "@Score", score);
                DbHelper.AddParam(cmd, "@TotalMarks", totalMarks);
                DbHelper.AddParam(cmd, "@IsPassed", isPassed);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}