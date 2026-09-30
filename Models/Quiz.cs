namespace SkillCore.Models
{
    public class Quiz
    {
        public int QuizID { get; set; }
        public int CourseID { get; set; }
        public string Title { get; set; }
        public int PassMark { get; set; }
    }
}