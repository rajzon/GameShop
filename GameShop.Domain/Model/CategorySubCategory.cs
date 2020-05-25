namespace GameShop.Domain.Model
{
    public class CategorySubCategory
    {
        public virtual Category Category { get; set; }
        public int CategoryId { get; set; }
        public virtual SubCategory SubCategory { get; set; }
        public int SubCategoryId { get; set; }
    }
}