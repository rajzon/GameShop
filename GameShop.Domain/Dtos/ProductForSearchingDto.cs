using System.Collections.Generic;
using GameShop.Domain.Model;

namespace GameShop.Domain.Dtos
{
    public class ProductForSearchingDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public Photo Photo { get; set; }
        public string CategoryName { get; set; }
    }
}