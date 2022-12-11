using System.ComponentModel.DataAnnotations;

namespace BasketballAppSoftuni.Web.Models.UserAccountViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Please enter a username between 3 and 20 symbols!")]
        public string Username { get; set; } = null!;

        [Required]
        [EmailAddress]
        [StringLength(60, MinimumLength = 5, ErrorMessage = "Please enter an email between 5 and 60 symbols!")]
        public string Email { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        [StringLength(30, MinimumLength = 8, ErrorMessage = "Please enter a password between 8 and 30 symbols!")]
        public string Password { get; set; } = null!;

        [Compare(nameof(Password), ErrorMessage = "Passwords dont match!")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; } = null!;
    }
}
