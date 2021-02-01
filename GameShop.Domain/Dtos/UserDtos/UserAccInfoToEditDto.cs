using System.ComponentModel.DataAnnotations;

namespace GameShop.Domain.Dtos.UserDtos
{
    public class UserAccInfoToEditDto
    {

        [Required]
        public string Name { get; set; }
        [Required]
        [StringLength(40)]
        public string SurName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
    }
}