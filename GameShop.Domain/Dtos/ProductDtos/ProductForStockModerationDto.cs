namespace GameShop.Domain.Dtos.ProductDtos
{
    public class ProductForStockModerationDto
    {
         public int Id { get; set; }
         public string Name { get; set; }
         public string CategoryName { get; set; }
         public int StockQuantity { get; set; }
    }
}