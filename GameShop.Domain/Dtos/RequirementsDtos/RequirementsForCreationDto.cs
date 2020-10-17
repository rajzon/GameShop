using System.ComponentModel.DataAnnotations;

namespace GameShop.Domain.Dtos
{
    public class RequirementsForCreationDto
    {
        [StringLength(30)]
        public string OS { get; set; }
        [StringLength(100)]
        public string Processor { get; set; }
        public byte RAM { get; set; }
        [StringLength(100)]
        public string GraphicsCard { get; set; }
        public ushort HDD { get; set; }
        public bool IsNetworkConnectionRequire { get; set; }
    }
}