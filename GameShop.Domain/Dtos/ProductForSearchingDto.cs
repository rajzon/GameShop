namespace GameShop.Domain.Dtos
{
    public class ProductForSearchingDto
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string[] PhotosUrl { get; set; }
        public string CategoryName { get; set; }
    }
}