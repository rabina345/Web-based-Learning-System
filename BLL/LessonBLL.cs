using System.Collections.Generic;
using SkillCore.Data_Access_Layer;
using SkillCore.Models;

namespace SkillCore.BLL
{
    public static class LessonBLL
    {
        public static List<Lesson> GetByCourseId(int courseId)
        {
            return LessonDAL.GetByCourseId(courseId);
        }

        public static Lesson GetById(int lessonId)
        {
            return LessonDAL.GetById(lessonId);
        }

        public static void Save(Lesson l)
        {
            if (string.IsNullOrWhiteSpace(l.Title))
                throw new System.ArgumentException("Title is required.");
            if (l.CourseID <= 0)
                throw new System.ArgumentException("Please choose a course.");

            if (l.LessonID == 0)
                LessonDAL.Insert(l);
            else
                LessonDAL.Update(l);
        }

        public static void Delete(int lessonId)
        {
            LessonDAL.Delete(lessonId);
        }
    }
}