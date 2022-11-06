using System.ComponentModel.DataAnnotations;

namespace BasketballAppSoftuni.Models
{
    public class RegisterViewModel
    {
        [Required]
        [StringLength(20,MinimumLength =3)]
        public string Username { get; set; } = null!;

        [Required]
        [EmailAddress]
        [StringLength(60, MinimumLength = 5)]
        public string Email { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        [StringLength(30,MinimumLength =8)]
        public string Password { get; set; } = null!;

        [Compare(nameof(Password))]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; } = null!;
    }
}
