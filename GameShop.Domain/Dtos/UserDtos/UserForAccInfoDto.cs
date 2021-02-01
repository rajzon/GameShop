using System.ComponentModel.DataAnnotations;

namespace GameShop.Domain.Dtos.UserDtos
{
    public class UserForAccInfoDto
    {
        public string Name { get; set; }
        public string SurName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
    }
}