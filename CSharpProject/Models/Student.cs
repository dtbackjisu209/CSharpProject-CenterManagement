using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSharpProject.Models
{
    public class Student
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("StudentID")]
        public int StudentID { get; set; }


        [Required]
        [MaxLength(25)]
        [Column("StudentName")]
        public string StudentName { get; set; }= string.Empty;

        [Required]
        [Column("StudentBirth")]
        public DateTime StudentBirth { get; set; }

        [Required]
        [MaxLength(15)]
        [Column("StudentPhone")]
        public string StudentPhone { get; set; }= string.Empty;

        [Required]
        [Column("StudentEmail")]
        public string StudentEmail { get; set; }= string.Empty;

        [Required]
        [MaxLength(25)]
        [Column("StudentAccount")]
        public string StudentAccount { get; set; }= string.Empty;

        [Required]
        [MaxLength(25)]
        [Column("StudentPass")]
        public string StudentPass { get; set; }= string.Empty;
        public List<Enrollment> Dkikhs { get; set; }= new List<Enrollment>();

    }
}
