using System;
using System.ComponentModel.DataAnnotations;

namespace GameShop.Domain.Dtos
{
    public class ProductToEditDto
    {
        // public int Id { get; set; }
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
       // [Required]
        // public RequirementsForCreationDto  Requirements { get; set; }
        [Required]
        public RequirementsForEditDto  Requirements { get; set; }
        [Required]
        public int CategoryId { get; set; }
        //[Required]
        public int[] LanguagesId { get; set; }
        //[Required]
        public int[] SubCategoriesId { get; set; }       
        //public string[] Photos { get; set; }
    }
}