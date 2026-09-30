using System;

namespace SkillCore.Models
{
    public class LessonProgress
    {
        public int ProgressID { get; set; }
        public int UserID { get; set; }
        public int LessonID { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime? CompletedAt { get; set; }
    }
}