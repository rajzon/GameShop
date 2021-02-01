using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace GameShop.Domain.Validators
{
    public class DeliveryTypeAttribute : ValidationAttribute
    {
        private readonly string[] _allowedTypes;

        public DeliveryTypeAttribute(params string[] allowedTypes)
        {
            _allowedTypes = allowedTypes;    
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var type = value as string;

            if(type == null || !_allowedTypes.Contains(type))
            {
                return new ValidationResult("Not valid delivery type");
            }

            return ValidationResult.Success;

        }
    }
}