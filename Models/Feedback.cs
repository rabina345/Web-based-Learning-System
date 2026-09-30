using System;

namespace SkillCore.Models
{
    public class Feedback
    {
        public int FeedbackID { get; set; }
        public int? UserID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public bool IsRead { get; set; }
        public DateTime SubmittedAt { get; set; }
    }
}