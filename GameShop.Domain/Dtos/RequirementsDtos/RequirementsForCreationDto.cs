namespace GameShop.Domain.Dtos
{
    public class RequirementsForCreationDto
    {
        public string OS { get; set; }
        public string Processor { get; set; }
        public byte RAM { get; set; }
        public string GraphicsCard { get; set; }
        public ushort HDD { get; set; }
        public bool IsNetworkConnectionRequire { get; set; }
    }
}