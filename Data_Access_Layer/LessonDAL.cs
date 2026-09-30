using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using SkillCore.Models;

namespace SkillCore.Data_Access_Layer
{
    public static class LessonDAL
    {
        public static List<Lesson> GetByCourseId(int courseId)
        {
            List<Lesson> lessons = new List<Lesson>();
            using (SqlConnection con = DbHelper.GetConnection())
            {
                string sql = @"SELECT * FROM Lessons WHERE CourseID = @CourseID ORDER BY SortOrder";
                SqlCommand cmd = DbHelper.CreateCommand(con, sql);
                DbHelper.AddParam(cmd, "@CourseID", courseId);
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lessons.Add(MapLesson(reader));
                    }
                }
            }
            return lessons;
        }

        private static Lesson MapLesson(IDataRecord r)
        {
            return new Lesson
            {
                LessonID = DbHelper.GetInt(r, "LessonID"),
                CourseID = DbHelper.GetInt(r, "CourseID"),
                Title = DbHelper.GetString(r, "Title"),
                ContentHtml = DbHelper.GetString(r, "ContentHtml"),
                SortOrder = DbHelper.GetInt(r, "SortOrder"),
                DurationMinutes = DbHelper.GetNullableInt(r, "DurationMinutes")
            };
        }

        public static Lesson GetById(int lessonId)
        {
            using (SqlConnection con = DbHelper.GetConnection())
            {
                SqlCommand cmd = DbHelper.CreateCommand(con, "SELECT * FROM Lessons WHERE LessonID = @LessonID");
                DbHelper.AddParam(cmd, "@LessonID", lessonId);
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return MapLesson(reader);
                    }
                }
            }
            return null;
        }

        public static void Insert(Lesson l)
        {
            using (SqlConnection con = DbHelper.GetConnection())
            {
                string sql = @"INSERT INTO Lessons (CourseID, Title, ContentHtml, SortOrder, DurationMinutes)
                        VALUES (@CourseID, @Title, @ContentHtml, @SortOrder, @DurationMinutes)";
                SqlCommand cmd = DbHelper.CreateCommand(con, sql);
                DbHelper.AddParam(cmd, "@CourseID", l.CourseID);
                DbHelper.AddParam(cmd, "@Title", l.Title);
                DbHelper.AddParam(cmd, "@ContentHtml", l.ContentHtml);
                DbHelper.AddParam(cmd, "@SortOrder", l.SortOrder);
                DbHelper.AddParam(cmd, "@DurationMinutes", l.DurationMinutes);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static void Update(Lesson l)
        {
            using (SqlConnection con = DbHelper.GetConnection())
            {
                string sql = @"UPDATE Lessons SET CourseID=@CourseID, Title=@Title, ContentHtml=@ContentHtml,
                        SortOrder=@SortOrder, DurationMinutes=@DurationMinutes WHERE LessonID=@LessonID";
                SqlCommand cmd = DbHelper.CreateCommand(con, sql);
                DbHelper.AddParam(cmd, "@CourseID", l.CourseID);
                DbHelper.AddParam(cmd, "@Title", l.Title);
                DbHelper.AddParam(cmd, "@ContentHtml", l.ContentHtml);
                DbHelper.AddParam(cmd, "@SortOrder", l.SortOrder);
                DbHelper.AddParam(cmd, "@DurationMinutes", l.DurationMinutes);
                DbHelper.AddParam(cmd, "@LessonID", l.LessonID);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static void Delete(int lessonId)
        {
            using (SqlConnection con = DbHelper.GetConnection())
            {
                SqlCommand cmd = DbHelper.CreateCommand(con, "DELETE FROM Lessons WHERE LessonID = @LessonID");
                DbHelper.AddParam(cmd, "@LessonID", lessonId);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // All lessons across every course, with the course title attached — for the admin list
        public static List<Lesson> GetAllWithCourseTitle(out Dictionary<int, string> courseTitles)
        {
            List<Lesson> lessons = new List<Lesson>();
            courseTitles = new Dictionary<int, string>();
            using (SqlConnection con = DbHelper.GetConnection())
            {
                string sql = @"SELECT l.*, c.Title AS CourseTitle FROM Lessons l
                        JOIN Courses c ON l.CourseID = c.CourseID
                        ORDER BY c.Title, l.SortOrder";
                SqlCommand cmd = DbHelper.CreateCommand(con, sql);
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Lesson l = MapLesson(reader);
                        lessons.Add(l);
                        courseTitles[l.LessonID] = DbHelper.GetString(reader, "CourseTitle");
                    }
                }
            }
            return lessons;
        }


    }
}