namespace GameShop.Domain.Dtos
{
    public class ProductCollectionPropertiesDto
    {
        public int[] LanguagesId { get; set; }
        //[Required]
        public int[] SubCategoriesId { get; set; }       
        public string[] Photos { get; set; }
    }
}