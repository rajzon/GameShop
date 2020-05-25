namespace GameShop.Domain.Model
{
    public class ProductSubCategory
    {
        public virtual Product Product { get; set; }
        public int ProductId { get; set; }
        public virtual SubCategory SubCategory { get; set; }
        public int SubCategoryId { get; set; }
    }
}