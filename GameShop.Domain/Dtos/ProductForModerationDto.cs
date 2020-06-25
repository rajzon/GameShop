using System;
using System.Collections.Generic;

namespace GameShop.Domain.Dtos
{
    public class ProductForModerationDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public ICollection<string> SubCategories { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string CategoryName { get; set; }
    }
}