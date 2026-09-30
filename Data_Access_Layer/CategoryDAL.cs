using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using SkillCore.Models;

namespace SkillCore.Data_Access_Layer
{
    public static class CategoryDAL
    {
        public static List<Category> GetAll()
        {
            List<Category> categories = new List<Category>();
            using (SqlConnection con = DbHelper.GetConnection())
            {
                SqlCommand cmd = DbHelper.CreateCommand(con, "SELECT * FROM Categories ORDER BY CategoryName");
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        categories.Add(new Category
                        {
                            CategoryID = DbHelper.GetInt(reader, "CategoryID"),
                            CategoryName = DbHelper.GetString(reader, "CategoryName"),
                            Description = DbHelper.GetString(reader, "Description")
                        });
                    }
                }
            }
            return categories;
        }

        public static void Insert(Category c)
        {
            using (SqlConnection con = DbHelper.GetConnection())
            {
                SqlCommand cmd = DbHelper.CreateCommand(con,
                    "INSERT INTO Categories (CategoryName, Description) VALUES (@CategoryName, @Description)");
                DbHelper.AddParam(cmd, "@CategoryName", c.CategoryName);
                DbHelper.AddParam(cmd, "@Description", c.Description);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static void Update(Category c)
        {
            using (SqlConnection con = DbHelper.GetConnection())
            {
                SqlCommand cmd = DbHelper.CreateCommand(con,
                    "UPDATE Categories SET CategoryName=@CategoryName, Description=@Description WHERE CategoryID=@CategoryID");
                DbHelper.AddParam(cmd, "@CategoryName", c.CategoryName);
                DbHelper.AddParam(cmd, "@Description", c.Description);
                DbHelper.AddParam(cmd, "@CategoryID", c.CategoryID);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static void Delete(int categoryId)
        {
            using (SqlConnection con = DbHelper.GetConnection())
            {
                SqlCommand cmd = DbHelper.CreateCommand(con, "DELETE FROM Categories WHERE CategoryID = @CategoryID");
                DbHelper.AddParam(cmd, "@CategoryID", categoryId);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static Category GetById(int categoryId)
        {
            using (SqlConnection con = DbHelper.GetConnection())
            {
                SqlCommand cmd = DbHelper.CreateCommand(con, "SELECT * FROM Categories WHERE CategoryID = @CategoryID");
                DbHelper.AddParam(cmd, "@CategoryID", categoryId);
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Category
                        {
                            CategoryID = DbHelper.GetInt(reader, "CategoryID"),
                            CategoryName = DbHelper.GetString(reader, "CategoryName"),
                            Description = DbHelper.GetString(reader, "Description")
                        };
                    }
                }
            }
            return null;
        }
    }
}