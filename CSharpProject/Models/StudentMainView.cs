namespace CSharpProject.Models
{
    public class StudentMainView
    {
        public List<Course> courses { get; set; } = new List<Course>();
        public string FindingName { get; set; }
        public string StudentName { get; set; }
    }
}
