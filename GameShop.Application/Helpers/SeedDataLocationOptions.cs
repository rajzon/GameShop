namespace GameShop.Application.Helpers
{
    public class SeedDataLocationOptions
    {
        public const string SeedDataLocation = "SeedDataLocation";

        public string PhotoSeedData { get; set; }
        public string ProductSeedData { get; set; }
        public string RequirementsSeedData { get; set; }
        public string SubCategorySeedData { get; set; }
        public string UserSeedData { get; set; }
    }
}