using System.ComponentModel.DataAnnotations;

namespace LIT.Application.ViewModels
{
    public class UserViewModel
    {
        [Required(ErrorMessage = "UserName cannot be null or empty.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "UserName must be between 3 and 50 characters.")]
        public string? Name { get; set; } 

        [Required(ErrorMessage = "Password cannot be null or empty.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 100 characters.")]
        public string? Password { get; set; }
    }

    public class LoginViewModel : UserViewModel { }

    public class  LoginResultViewModel
    {
        public Guid UserId { get; set; }
        public string Username { get; set; }
    }

    public class LoginResponseViewModel
    {
        public ResultViewModel ResultViewModel { get; set; }
        public LoginResultViewModel? LoginResultViewModel { get; set; }
    }
}
