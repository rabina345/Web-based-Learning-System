using System;

namespace SkillCore.Models
{
    public class Portfolio
    {
        public int PortfolioID { get; set; }
        public int UserID { get; set; }
        public int? CourseID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string FilePath { get; set; }
        public DateTime SubmittedAt { get; set; }
    }
}