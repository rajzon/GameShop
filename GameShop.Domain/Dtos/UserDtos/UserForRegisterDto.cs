using System.ComponentModel.DataAnnotations;

namespace GameShop.Domain.Dtos
{
    public class UserForRegisterDto
    {
        [Required]
        [StringLength(30 , MinimumLength = 3, ErrorMessage="Username must be specify between 3 and 30 characters.")]
        public string Username { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(8 , MinimumLength = 4, ErrorMessage="Password must be specify between 4 and 8 characters.")]
        public string Password { get; set; }
    }
}