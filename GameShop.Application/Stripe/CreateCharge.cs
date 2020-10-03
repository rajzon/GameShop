using System.Collections.Generic;
using GameShop.Application.Interfaces;
using GameShop.Domain.Dtos.BasketDtos;
using Stripe;

namespace GameShop.Domain.Logic
{
    public class CreateCharge : ICreateCharge
    {
        public BasketForPaymentDto Do(string stripeToken, decimal basketPrice, List<ProductFromBasketCookieDto> basketProductsCookie)
        {
            var customers = new CustomerService();
            var charges = new ChargeService();

            var customer = customers.Create(new CustomerCreateOptions
            {
                Source = stripeToken
            });

            var charge = charges.Create(new ChargeCreateOptions
            {
                Amount = (int)(basketPrice * 100),
                Description = "Test Desc",
                Currency = "usd",
                Customer = customer.Id,

            });
            
            var basketForPaymentDto = new BasketForPaymentDto()
            {
                BasketPrice = basketPrice,
                StripeRef = charge.Id,
                BasketProducts = basketProductsCookie
            };

            return basketForPaymentDto;
        }
    }
}