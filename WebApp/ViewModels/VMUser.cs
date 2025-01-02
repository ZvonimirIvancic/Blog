using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModels
{
    public class VMUser
    {
        public int Idusers { get; set; }

        [DisplayName("Username")]
        [Required(ErrorMessage = "User name is required")]
        public string Username { get; set; } = null!;


        [DisplayName("Password")]
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = null!;


        [DisplayName("User Email Adress")]
        [Required, EmailAddress(ErrorMessage = "User Email adress is required")]
        public string Email { get; set; } = null!;
    }
}
