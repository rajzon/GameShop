using System.ComponentModel.DataAnnotations;

namespace GameShop.Domain.Dtos
{
    public class UserForRegisterDto
    {
        [Required]
        [StringLength(30 , MinimumLength = 3, ErrorMessage="Username must be specify between 3 and 30 characters.")]
        public string Username { get; set; }
        [Required]
        [StringLength(40 , MinimumLength = 3, ErrorMessage="Surname must be specify between 3 and 40 characters.")]
        public string SurName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(8 , MinimumLength = 4, ErrorMessage="Password must be specify between 4 and 8 characters.")]
        public string Password { get; set; }

        private string _phone;
        [Phone]
        public string Phone { get { return _phone; } set{ _phone = string.IsNullOrWhiteSpace(value)? null: value;} }
    }
}