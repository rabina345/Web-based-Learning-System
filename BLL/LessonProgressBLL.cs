using SkillCore.Data_Access_Layer;

namespace SkillCore.BLL
{
    public static class LessonProgressBLL
    {
        public static bool IsCompleted(int userId, int lessonId)
        {
            return LessonProgressDAL.IsCompleted(userId, lessonId);
        }

        public static void MarkComplete(int userId, int lessonId)
        {
            LessonProgressDAL.MarkComplete(userId, lessonId);
        }
    }
}