using GameShop.Domain.Dtos.BasketDtos;
using GameShop.Domain.Dtos.CustomerDto;

namespace GameShop.Domain.Dtos.PaymentDto
{
    public class PaymentInfoForChargeDto
    {
        public BasketForPaymentDto Basket { get; set; }
        public CustomerInfoDto CustomerInfo { get; set; } 
    }
}