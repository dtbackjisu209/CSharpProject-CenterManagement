using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSharpProject.Models
{
    [Table("course")]
    public class Course
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("CourseID")]
        public int CourseID { get; set; }

        [Required]
        [MaxLength(25)]
        [Column("CourseName")]
        public string CourseName { get; set; }

        [Required]
        [MaxLength(30)]
        [Column("CourseTeacher")]
        public string CourseTeacher { get; set; }

        [Required]
        [Column("CourseDate")]
        public DateTime CourseDate { get; set; }

        [Required]
        [Column("CourseTuition")]
        public int CourseTuition { get; set; }

        [Required]
        [Column("CourseNumber")]
        public int CourseNumber { get; set; }
        public List<Enrollment> Dkikhs { get; set; } = new List<Enrollment>();

    }
}
