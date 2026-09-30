using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using SkillCore.Models;

namespace SkillCore.Data_Access_Layer
{
    public static class EnrollmentDAL
    {
        public static bool IsEnrolled(int userId, int courseId)
        {
            using (SqlConnection con = DbHelper.GetConnection())
            {
                SqlCommand cmd = DbHelper.CreateCommand(con,
                    "SELECT COUNT(*) FROM Enrollments WHERE UserID = @UserID AND CourseID = @CourseID");
                DbHelper.AddParam(cmd, "@UserID", userId);
                DbHelper.AddParam(cmd, "@CourseID", courseId);
                con.Open();
                return (int)cmd.ExecuteScalar() > 0;
            }
        }

        public static void Enroll(int userId, int courseId)
        {
            using (SqlConnection con = DbHelper.GetConnection())
            {
                SqlCommand cmd = DbHelper.CreateCommand(con,
                    "INSERT INTO Enrollments (UserID, CourseID) VALUES (@UserID, @CourseID)");
                DbHelper.AddParam(cmd, "@UserID", userId);
                DbHelper.AddParam(cmd, "@CourseID", courseId);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // Courses a specific student is enrolled in, with course details joined in
        public static List<Course> GetEnrolledCourses(int userId)
        {
            List<Course> courses = new List<Course>();
            using (SqlConnection con = DbHelper.GetConnection())
            {
                string sql = @"SELECT c.*, cat.CategoryName FROM Enrollments e
                                JOIN Courses c ON e.CourseID = c.CourseID
                                JOIN Categories cat ON c.CategoryID = cat.CategoryID
                                WHERE e.UserID = @UserID
                                ORDER BY e.EnrolledAt DESC";
                SqlCommand cmd = DbHelper.CreateCommand(con, sql);
                DbHelper.AddParam(cmd, "@UserID", userId);
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        courses.Add(new Course
                        {
                            CourseID = DbHelper.GetInt(reader, "CourseID"),
                            Title = DbHelper.GetString(reader, "Title"),
                            ShortDescription = DbHelper.GetString(reader, "ShortDescription"),
                            CategoryID = DbHelper.GetInt(reader, "CategoryID"),
                            DifficultyLevel = DbHelper.GetString(reader, "DifficultyLevel"),
                            CategoryName = DbHelper.GetString(reader, "CategoryName")
                        });
                    }
                }
            }
            return courses;
        }
    }
}