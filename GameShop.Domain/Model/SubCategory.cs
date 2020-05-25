using System.Collections.Generic;

namespace GameShop.Domain.Model
{
    public class SubCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<CategorySubCategory> Categories { get; set; }
        public virtual ICollection<ProductSubCategory> Products { get; set; }   
    }
}