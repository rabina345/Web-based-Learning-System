using System;

namespace SkillCore.Models
{
    public class Course
    {
        public int CourseID { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string FullDescription { get; set; }
        public int CategoryID { get; set; }
        public string DifficultyLevel { get; set; }
        public int? DurationMinutes { get; set; }
        public bool IsPublished { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }

        // Joined field — filled in by the DAL when it joins to Categories
        public string CategoryName { get; set; }
    }
}