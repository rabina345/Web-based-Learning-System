using SkillCore.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SkillCore.Data_Access_Layer
{
    public static class UserDAL
    {
        // Used at registration to reject a duplicate email up front
        public static bool EmailExists(string email)
        {
            using (SqlConnection con = DbHelper.GetConnection())
            {
                SqlCommand cmd = DbHelper.CreateCommand(con, "SELECT COUNT(*) FROM Users WHERE Email = @Email");
                DbHelper.AddParam(cmd, "@Email", email);
                con.Open();
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }

        public static void Insert(User user)
        {
            using (SqlConnection con = DbHelper.GetConnection())
            {
                string sql = @"INSERT INTO Users (FullName, Email, PasswordHash, PasswordSalt, RoleID, IsActive)
                                VALUES (@FullName, @Email, @PasswordHash, @PasswordSalt, @RoleID, @IsActive)";
                SqlCommand cmd = DbHelper.CreateCommand(con, sql);
                DbHelper.AddParam(cmd, "@FullName", user.FullName);
                DbHelper.AddParam(cmd, "@Email", user.Email);
                DbHelper.AddParam(cmd, "@PasswordHash", user.PasswordHash);
                DbHelper.AddParam(cmd, "@PasswordSalt", user.PasswordSalt);
                DbHelper.AddParam(cmd, "@RoleID", user.RoleID);
                DbHelper.AddParam(cmd, "@IsActive", user.IsActive);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // Used at login — returns null if no account has that email
        public static User GetByEmail(string email)
        {
            using (SqlConnection con = DbHelper.GetConnection())
            {
                SqlCommand cmd = DbHelper.CreateCommand(con, "SELECT * FROM Users WHERE Email = @Email");
                DbHelper.AddParam(cmd, "@Email", email);
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return MapUser(reader);
                    }
                }
            }
            return null;
        }

        private static User MapUser(IDataRecord r)
        {
            return new User
            {
                UserID = DbHelper.GetInt(r, "UserID"),
                FullName = DbHelper.GetString(r, "FullName"),
                Email = DbHelper.GetString(r, "Email"),
                PasswordHash = DbHelper.GetString(r, "PasswordHash"),
                PasswordSalt = DbHelper.GetString(r, "PasswordSalt"),
                RoleID = DbHelper.GetInt(r, "RoleID"),
                IsActive = DbHelper.GetBool(r, "IsActive"),
                CreatedAt = DbHelper.GetDate(r, "CreatedAt")
            };
        }

        public static List<User> GetAll()
        {
            List<User> users = new List<User>();
            using (SqlConnection con = DbHelper.GetConnection())
            {
                SqlCommand cmd = DbHelper.CreateCommand(con, "SELECT * FROM Users ORDER BY FullName");
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        users.Add(MapUser(reader));
                    }
                }
            }
            return users;
        }

        public static User GetById(int userId)
        {
            using (SqlConnection con = DbHelper.GetConnection())
            {
                SqlCommand cmd = DbHelper.CreateCommand(con, "SELECT * FROM Users WHERE UserID = @UserID");
                DbHelper.AddParam(cmd, "@UserID", userId);
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return MapUser(reader);
                    }
                }
            }
            return null;
        }

        public static void UpdateRoleAndStatus(int userId, int roleId, bool isActive)
        {
            using (SqlConnection con = DbHelper.GetConnection())
            {
                SqlCommand cmd = DbHelper.CreateCommand(con,
                    "UPDATE Users SET RoleID = @RoleID, IsActive = @IsActive WHERE UserID = @UserID");
                DbHelper.AddParam(cmd, "@RoleID", roleId);
                DbHelper.AddParam(cmd, "@IsActive", isActive);
                DbHelper.AddParam(cmd, "@UserID", userId);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static void Delete(int userId)
        {
            using (SqlConnection con = DbHelper.GetConnection())
            {
                SqlCommand cmd = DbHelper.CreateCommand(con, "DELETE FROM Users WHERE UserID = @UserID");
                DbHelper.AddParam(cmd, "@UserID", userId);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}