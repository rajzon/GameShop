using System;
using System.ComponentModel.DataAnnotations;

namespace GameShop.Domain.Dtos.ProductDtos
{
    public class ProductEditDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public byte Pegi { get; set; }
        [Required]
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