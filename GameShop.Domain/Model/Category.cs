using System.Collections.Generic;

namespace GameShop.Domain.Model
{   
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<CategorySubCategory> SubCategories { get; set; }
    }
}