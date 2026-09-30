using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using SkillCore.Models;

namespace SkillCore.Data_Access_Layer
{
    public static class PortfolioDAL
    {
        public static List<Portfolio> GetByUserId(int userId)
        {
            List<Portfolio> items = new List<Portfolio>();
            using (SqlConnection con = DbHelper.GetConnection())
            {
                SqlCommand cmd = DbHelper.CreateCommand(con,
                    "SELECT * FROM Portfolios WHERE UserID = @UserID ORDER BY SubmittedAt DESC");
                DbHelper.AddParam(cmd, "@UserID", userId);
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        items.Add(MapPortfolio(reader));
                    }
                }
            }
            return items;
        }

        public static Portfolio GetById(int portfolioId)
        {
            using (SqlConnection con = DbHelper.GetConnection())
            {
                SqlCommand cmd = DbHelper.CreateCommand(con, "SELECT * FROM Portfolios WHERE PortfolioID = @PortfolioID");
                DbHelper.AddParam(cmd, "@PortfolioID", portfolioId);
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return MapPortfolio(reader);
                    }
                }
            }
            return null;
        }

        public static void Insert(Portfolio p)
        {
            using (SqlConnection con = DbHelper.GetConnection())
            {
                string sql = @"INSERT INTO Portfolios (UserID, CourseID, Title, Description, FilePath)
                                VALUES (@UserID, @CourseID, @Title, @Description, @FilePath)";
                SqlCommand cmd = DbHelper.CreateCommand(con, sql);
                DbHelper.AddParam(cmd, "@UserID", p.UserID);
                DbHelper.AddParam(cmd, "@CourseID", p.CourseID);
                DbHelper.AddParam(cmd, "@Title", p.Title);
                DbHelper.AddParam(cmd, "@Description", p.Description);
                DbHelper.AddParam(cmd, "@FilePath", p.FilePath);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static void Update(Portfolio p)
        {
            using (SqlConnection con = DbHelper.GetConnection())
            {
                string sql = @"UPDATE Portfolios SET Title=@Title, Description=@Description, FilePath=@FilePath
                                WHERE PortfolioID=@PortfolioID";
                SqlCommand cmd = DbHelper.CreateCommand(con, sql);
                DbHelper.AddParam(cmd, "@Title", p.Title);
                DbHelper.AddParam(cmd, "@Description", p.Description);
                DbHelper.AddParam(cmd, "@FilePath", p.FilePath);
                DbHelper.AddParam(cmd, "@PortfolioID", p.PortfolioID);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static void Delete(int portfolioId)
        {
            using (SqlConnection con = DbHelper.GetConnection())
            {
                SqlCommand cmd = DbHelper.CreateCommand(con, "DELETE FROM Portfolios WHERE PortfolioID = @PortfolioID");
                DbHelper.AddParam(cmd, "@PortfolioID", portfolioId);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private static Portfolio MapPortfolio(IDataRecord r)
        {
            return new Portfolio
            {
                PortfolioID = DbHelper.GetInt(r, "PortfolioID"),
                UserID = DbHelper.GetInt(r, "UserID"),
                CourseID = DbHelper.GetNullableInt(r, "CourseID"),
                Title = DbHelper.GetString(r, "Title"),
                Description = DbHelper.GetString(r, "Description"),
                FilePath = DbHelper.GetString(r, "FilePath"),
                SubmittedAt = DbHelper.GetDate(r, "SubmittedAt")
            };
        }

        // All portfolio items across every student, with the uploader's name attached
        public static List<Portfolio> GetAllWithUserName(out Dictionary<int, string> uploaderNames)
        {
            List<Portfolio> items = new List<Portfolio>();
            uploaderNames = new Dictionary<int, string>();

            using (SqlConnection con = DbHelper.GetConnection())
            {
                string sql = @"SELECT p.*, u.FullName FROM Portfolios p
                        JOIN Users u ON p.UserID = u.UserID
                        ORDER BY p.SubmittedAt DESC";
                SqlCommand cmd = DbHelper.CreateCommand(con, sql);
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Portfolio p = MapPortfolio(reader);
                        items.Add(p);
                        uploaderNames[p.PortfolioID] = DbHelper.GetString(reader, "FullName");
                    }
                }
            }
            return items;
        }
    }
}