using System.ComponentModel.DataAnnotations;

namespace GameShop.Domain.Model
{
    public class Address
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Street { get; set; }
        public string PostCode { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        
        public User User { get; set; }
        public int UserId { get; set; }
    }
}