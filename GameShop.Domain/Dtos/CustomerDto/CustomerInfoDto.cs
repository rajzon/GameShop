using System.ComponentModel.DataAnnotations;

namespace GameShop.Domain.Dtos.CustomerDto
{
    public class CustomerInfoDto
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        [MaxLength(30)]
        public string SurName { get; set; }

        [Required]
        [MaxLength(80)]
        public string Address { get; set; }

        [Required]
        [MaxLength(80)]
        public string Street { get; set; }

        [Required]
        [MaxLength(6)]
        //TO DO: implement post code validation
        public string PostCode { get; set; }
        
        [MaxLength(40)]
        public string City { get; set; }
    }
}