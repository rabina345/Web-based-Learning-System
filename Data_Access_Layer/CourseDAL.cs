using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using SkillCore.Models;

namespace SkillCore.Data_Access_Layer
{
    public static class CourseDAL
    {
        // Only published courses, newest category name attached via JOIN
        public static List<Course> GetAllPublished()
        {
            List<Course> courses = new List<Course>();
            using (SqlConnection con = DbHelper.GetConnection())
            {
                string sql = @"SELECT c.*, cat.CategoryName
                                FROM Courses c
                                JOIN Categories cat ON c.CategoryID = cat.CategoryID
                                WHERE c.IsPublished = 1
                                ORDER BY c.Title";
                SqlCommand cmd = DbHelper.CreateCommand(con, sql);
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        courses.Add(MapCourse(reader));
                    }
                }
            }
            return courses;
        }

        public static void Insert(Course c)
        {
            using (SqlConnection con = DbHelper.GetConnection())
            {
                string sql = @"INSERT INTO Courses (Title, ShortDescription, FullDescription, CategoryID, DifficultyLevel, DurationMinutes, IsPublished, CreatedBy)
                        VALUES (@Title, @ShortDescription, @FullDescription, @CategoryID, @DifficultyLevel, @DurationMinutes, @IsPublished, @CreatedBy)";
                SqlCommand cmd = DbHelper.CreateCommand(con, sql);
                DbHelper.AddParam(cmd, "@Title", c.Title);
                DbHelper.AddParam(cmd, "@ShortDescription", c.ShortDescription);
                DbHelper.AddParam(cmd, "@FullDescription", c.FullDescription);
                DbHelper.AddParam(cmd, "@CategoryID", c.CategoryID);
                DbHelper.AddParam(cmd, "@DifficultyLevel", c.DifficultyLevel);
                DbHelper.AddParam(cmd, "@DurationMinutes", c.DurationMinutes);
                DbHelper.AddParam(cmd, "@IsPublished", c.IsPublished);
                DbHelper.AddParam(cmd, "@CreatedBy", c.CreatedBy);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static void Update(Course c)
        {
            using (SqlConnection con = DbHelper.GetConnection())
            {
                string sql = @"UPDATE Courses SET Title=@Title, ShortDescription=@ShortDescription,
                        FullDescription=@FullDescription, CategoryID=@CategoryID,
                        DifficultyLevel=@DifficultyLevel, DurationMinutes=@DurationMinutes,
                        IsPublished=@IsPublished WHERE CourseID=@CourseID";
                SqlCommand cmd = DbHelper.CreateCommand(con, sql);
                DbHelper.AddParam(cmd, "@Title", c.Title);
                DbHelper.AddParam(cmd, "@ShortDescription", c.ShortDescription);
                DbHelper.AddParam(cmd, "@FullDescription", c.FullDescription);
                DbHelper.AddParam(cmd, "@CategoryID", c.CategoryID);
                DbHelper.AddParam(cmd, "@DifficultyLevel", c.DifficultyLevel);
                DbHelper.AddParam(cmd, "@DurationMinutes", c.DurationMinutes);
                DbHelper.AddParam(cmd, "@IsPublished", c.IsPublished);
                DbHelper.AddParam(cmd, "@CourseID", c.CourseID);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static void Delete(int courseId)
        {
            using (SqlConnection con = DbHelper.GetConnection())
            {
                SqlCommand cmd = DbHelper.CreateCommand(con, "DELETE FROM Courses WHERE CourseID = @CourseID");
                DbHelper.AddParam(cmd, "@CourseID", courseId);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // Includes unpublished courses too — for the admin list
        public static List<Course> GetAll()
        {
            List<Course> courses = new List<Course>();
            using (SqlConnection con = DbHelper.GetConnection())
            {
                string sql = @"SELECT c.*, cat.CategoryName FROM Courses c
                        JOIN Categories cat ON c.CategoryID = cat.CategoryID
                        ORDER BY c.Title";
                SqlCommand cmd = DbHelper.CreateCommand(con, sql);
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        courses.Add(MapCourse(reader));
                    }
                }
            }
            return courses;
        }

        public static Course GetById(int courseId)
        {
            using (SqlConnection con = DbHelper.GetConnection())
            {
                string sql = @"SELECT c.*, cat.CategoryName
                                FROM Courses c
                                JOIN Categories cat ON c.CategoryID = cat.CategoryID
                                WHERE c.CourseID = @CourseID";
                SqlCommand cmd = DbHelper.CreateCommand(con, sql);
                DbHelper.AddParam(cmd, "@CourseID", courseId);
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return MapCourse(reader);
                    }
                }
            }
            return null;
        }

        private static Course MapCourse(IDataRecord r)
        {
            return new Course
            {
                CourseID = DbHelper.GetInt(r, "CourseID"),
                Title = DbHelper.GetString(r, "Title"),
                ShortDescription = DbHelper.GetString(r, "ShortDescription"),
                FullDescription = DbHelper.GetString(r, "FullDescription"),
                CategoryID = DbHelper.GetInt(r, "CategoryID"),
                DifficultyLevel = DbHelper.GetString(r, "DifficultyLevel"),
                DurationMinutes = DbHelper.GetNullableInt(r, "DurationMinutes"),
                IsPublished = DbHelper.GetBool(r, "IsPublished"),
                CreatedBy = DbHelper.GetInt(r, "CreatedBy"),
                CreatedAt = DbHelper.GetDate(r, "CreatedAt"),
                CategoryName = DbHelper.GetString(r, "CategoryName")
            };

        }
    }
}