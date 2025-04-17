using System.ComponentModel.DataAnnotations;

namespace CSharpProject.Models
{
    public class LoginViewModel
    {
        [Required]
        public string StudentAccount { get; set; }

        [Required]
      
        public string StudentPass { get; set; }
    }

}
