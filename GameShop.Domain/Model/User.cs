using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace GameShop.Domain.Model
{
    public class User : IdentityUser<int>
    {
       
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }   
        public ICollection<UserRole> UserRoles { get; set; } 
    }
}