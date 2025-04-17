using System.ComponentModel.DataAnnotations.Schema;

namespace CSharpProject.Models
{
    public class SignedCourseData
    {
        public int CourseID { get; set; }
        public int EnrollmentID { get; set; }
        public string CourseName { get; set; }
        public DateTime DateOpen { get; set; }
        public DateTime DateSignCourse { get; set; }
    }
}
