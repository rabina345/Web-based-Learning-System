using System.Collections.Generic;
using SkillCore.Data_Access_Layer;
using SkillCore.Models;

namespace SkillCore.BLL
{
    public static class CourseBLL
    {
        public static List<Course> GetAllPublished()
        {
            return CourseDAL.GetAllPublished();
        }

        public static List<Course> GetAll()
        {
            return CourseDAL.GetAll();
        }

        public static void Save(Course c)
        {
            if (string.IsNullOrWhiteSpace(c.Title))
                throw new System.ArgumentException("Title is required.");
            if (c.CategoryID <= 0)
                throw new System.ArgumentException("Please choose a category.");

            if (c.CourseID == 0)
                CourseDAL.Insert(c);
            else
                CourseDAL.Update(c);
        }

        public static void Delete(int courseId)
        {
            CourseDAL.Delete(courseId);
        }

        public static Course GetById(int courseId)
        {
            return CourseDAL.GetById(courseId);
        }
    }
}

