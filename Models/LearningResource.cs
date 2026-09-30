namespace SkillCore.Models
{
    public class LearningResource
    {
        public int ResourceID { get; set; }
        public int LessonID { get; set; }
        public string Title { get; set; }
        public string FilePath { get; set; }
        public string ResourceType { get; set; }
    }
}