using System.ComponentModel.DataAnnotations;
using GameShop.Domain.Validators;

namespace GameShop.Domain.Dtos.OrderInfoDtos
{
    public class OrderInfoDto
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        [MaxLength(30)]
        public string SurName { get; set; }

        [Required]
        [MaxLength(80)]
        public string Street { get; set; }

        [Required]
        [MaxLength(12)]
        //TO DO: implement post code validation
        public string PostCode { get; set; }

        [Required]
        [Phone]
        public string Phone { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        [MaxLength(40)]
        public string City { get; set; }
        [MaxLength(40)]
        public string Country { get; set; }

        [DeliveryType("DPD", "DHL", "INPOST", "DIGITAL_PRODUCT")]
        public string DeliveryType { get; set; }
    }
}