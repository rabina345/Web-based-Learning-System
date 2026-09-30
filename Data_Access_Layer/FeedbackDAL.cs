using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using SkillCore.Models;

namespace SkillCore.Data_Access_Layer
{
    public static class FeedbackDAL
    {
        public static List<Feedback> GetAll()
        {
            List<Feedback> list = new List<Feedback>();
            using (SqlConnection con = DbHelper.GetConnection())
            {
                SqlCommand cmd = DbHelper.CreateCommand(con, "SELECT * FROM Feedback ORDER BY SubmittedAt DESC");
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(MapFeedback(reader));
                    }
                }
            }
            return list;
        }

        public static void Insert(Feedback f)
        {
            using (SqlConnection con = DbHelper.GetConnection())
            {
                string sql = @"INSERT INTO Feedback (UserID, Name, Email, Subject, Message, IsRead)
                                VALUES (@UserID, @Name, @Email, @Subject, @Message, 0)";
                SqlCommand cmd = DbHelper.CreateCommand(con, sql);
                DbHelper.AddParam(cmd, "@UserID", f.UserID);
                DbHelper.AddParam(cmd, "@Name", f.Name);
                DbHelper.AddParam(cmd, "@Email", f.Email);
                DbHelper.AddParam(cmd, "@Subject", f.Subject);
                DbHelper.AddParam(cmd, "@Message", f.Message);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static void MarkAsRead(int feedbackId)
        {
            using (SqlConnection con = DbHelper.GetConnection())
            {
                SqlCommand cmd = DbHelper.CreateCommand(con, "UPDATE Feedback SET IsRead = 1 WHERE FeedbackID = @FeedbackID");
                DbHelper.AddParam(cmd, "@FeedbackID", feedbackId);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static void Delete(int feedbackId)
        {
            using (SqlConnection con = DbHelper.GetConnection())
            {
                SqlCommand cmd = DbHelper.CreateCommand(con, "DELETE FROM Feedback WHERE FeedbackID = @FeedbackID");
                DbHelper.AddParam(cmd, "@FeedbackID", feedbackId);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private static Feedback MapFeedback(IDataRecord r)
        {
            return new Feedback
            {
                FeedbackID = DbHelper.GetInt(r, "FeedbackID"),
                UserID = DbHelper.GetNullableInt(r, "UserID"),
                Name = DbHelper.GetString(r, "Name"),
                Email = DbHelper.GetString(r, "Email"),
                Subject = DbHelper.GetString(r, "Subject"),
                Message = DbHelper.GetString(r, "Message"),
                IsRead = DbHelper.GetBool(r, "IsRead"),
                SubmittedAt = DbHelper.GetDate(r, "SubmittedAt")
            };
        }
    }
}