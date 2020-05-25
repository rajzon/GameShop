using Microsoft.AspNetCore.Identity;



namespace GameShop.Domain.Model
{
    public class UserRole : IdentityUserRole<int>
    {
        public virtual User User { get; set; }  
        public virtual Role Role { get; set; }
    }
}