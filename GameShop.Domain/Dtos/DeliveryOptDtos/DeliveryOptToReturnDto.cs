namespace GameShop.Domain.Dtos.DeliveryOptDtos
{
    public class DeliveryOptToReturnDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Currency { get; set; }
        public string Description { get; set; }
    }
}