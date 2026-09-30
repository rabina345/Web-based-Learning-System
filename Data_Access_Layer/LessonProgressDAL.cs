using System.Data.SqlClient;

namespace SkillCore.Data_Access_Layer
{
    public static class LessonProgressDAL
    {
        public static bool IsCompleted(int userId, int lessonId)
        {
            using (SqlConnection con = DbHelper.GetConnection())
            {
                SqlCommand cmd = DbHelper.CreateCommand(con,
                    "SELECT COUNT(*) FROM LessonProgress WHERE UserID = @UserID AND LessonID = @LessonID AND IsCompleted = 1");
                DbHelper.AddParam(cmd, "@UserID", userId);
                DbHelper.AddParam(cmd, "@LessonID", lessonId);
                con.Open();
                return (int)cmd.ExecuteScalar() > 0;
            }
        }

        // Insert a new row, or update the existing one — a student might
        // revisit a lesson they already completed
        public static void MarkComplete(int userId, int lessonId)
        {
            using (SqlConnection con = DbHelper.GetConnection())
            {
                con.Open();

                SqlCommand checkCmd = DbHelper.CreateCommand(con,
                    "SELECT COUNT(*) FROM LessonProgress WHERE UserID = @UserID AND LessonID = @LessonID");
                DbHelper.AddParam(checkCmd, "@UserID", userId);
                DbHelper.AddParam(checkCmd, "@LessonID", lessonId);
                bool exists = (int)checkCmd.ExecuteScalar() > 0;

                SqlCommand cmd;
                if (exists)
                {
                    cmd = DbHelper.CreateCommand(con,
                        "UPDATE LessonProgress SET IsCompleted = 1, CompletedAt = SYSUTCDATETIME() WHERE UserID = @UserID AND LessonID = @LessonID");
                }
                else
                {
                    cmd = DbHelper.CreateCommand(con,
                        "INSERT INTO LessonProgress (UserID, LessonID, IsCompleted, CompletedAt) VALUES (@UserID, @LessonID, 1, SYSUTCDATETIME())");
                }
                DbHelper.AddParam(cmd, "@UserID", userId);
                DbHelper.AddParam(cmd, "@LessonID", lessonId);
                cmd.ExecuteNonQuery();
            }
        }
    }
}