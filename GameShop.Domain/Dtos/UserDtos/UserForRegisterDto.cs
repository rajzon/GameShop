using System.ComponentModel.DataAnnotations;

namespace GameShop.Domain.Dtos
{
    public class UserForRegisterDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(8 , MinimumLength = 4, ErrorMessage="Password must be specify between 4 and 8 characters")]
        public string Password { get; set; }
    }
}