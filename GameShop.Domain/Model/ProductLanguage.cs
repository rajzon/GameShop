namespace GameShop.Domain.Model
{
    public class ProductLanguage
    {
        public virtual Product Product { get; set; }
        public int ProductId { get; set; }
        public virtual Language Language { get; set; }
        public int LanguageId { get; set; }
    }
}