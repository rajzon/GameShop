using System;

namespace GameShop.Domain.Model
{
    public class Photo
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public DateTime DateAdded { get; set; }
        public bool isMain { get; set; }
        public virtual Product Product { get; set; }
        public int ProductId { get; set; }
        
    }
}