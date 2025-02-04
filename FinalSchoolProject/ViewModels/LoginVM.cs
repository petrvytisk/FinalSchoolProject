using System.ComponentModel.DataAnnotations;

namespace FinalSchoolProject.ViewModels {
    public class LoginVM {
        public bool Remember { get; set; }

        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        public string? ReturnUrl { get; set; }
    }
}
