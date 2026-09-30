using System;

namespace SkillCore.Models
{
    public class QuizAttempt
    {
        public int AttemptID { get; set; }
        public int UserID { get; set; }
        public int QuizID { get; set; }
        public int Score { get; set; }
        public int TotalMarks { get; set; }
        public bool IsPassed { get; set; }
        public DateTime AttemptedAt { get; set; }
    }
}