using System;
using System.Collections.Generic;

namespace GameShop.Domain.Model
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte Pegi { get; set; }
        public decimal Price { get; set; }    
        public bool IsDigitalMedia { get; set; }
        public DateTime ReleaseDate { get; set; }

        public virtual Requirements Requirements { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<ProductLanguage> Languages { get; set; }
        public virtual ICollection<ProductSubCategory> SubCategories { get; set; }
        public virtual ICollection<Photo> Photos { get; set; }
        public Stock Stock { get; set; }
    }
}