using System.Collections.Generic;
namespace GameShop.Domain.Model
{
    public class Language
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<ProductLanguage> Products { get; set; } 
    }
}