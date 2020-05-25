namespace GameShop.Domain.Model
{
    public class Requirements
    {
        public int Id { get; set; }
        public string OS { get; set; }
        public string Processor { get; set; }
        public byte RAM { get; set; }
        public string GraphicsCard { get; set; }
        public ushort HDD { get; set; }
        public bool IsNetworkConnectionRequire { get; set; }
        public virtual Product Product { get; set; }
        public int ProductId { get; set; }
    }
}