using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CSharpProject.Models
{
    [Table("ĐKIKH")]
    public class Enrollment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("EnrollmentId")]
        public int EnrollmentId { get; set; }
        [Column("CourseId")]
        public int CourseID { get; set; }
        [Column("StudentId")]
        public int StudentID { get; set; }
        [Column("DateSignCourse")]
        public DateTime DateSignCourse { get; set; } 
        public Student student { get; set; }

        public Course course { get; set; }

    }
}
