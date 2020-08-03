using System.Collections.Generic;

namespace GameShop.Domain.Dtos
{
    public class UserForListDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public ICollection<string> Roles { get; set; }
    }
}