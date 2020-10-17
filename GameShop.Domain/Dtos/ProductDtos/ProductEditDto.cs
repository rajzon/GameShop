using System;
using System.ComponentModel.DataAnnotations;

namespace GameShop.Domain.Dtos.ProductDtos
{
    public class ProductEditDto
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        [StringLength(2000)]
        public string Description { get; set; }
        [Required]
        public byte Pegi { get; set; }
        [Required]
        [Range(0, 9999999.99)]
        public decimal Price { get; set; }   
        [Required] 
        public bool IsDigitalMedia { get; set; }
        [Required]       
        public DateTime ReleaseDate { get; set; }
        [Required]
        public RequirementsForEditDto  Requirements { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public int[] LanguagesId { get; set; }
        [Required]
        public int[] SubCategoriesId { get; set; } 
    }
}