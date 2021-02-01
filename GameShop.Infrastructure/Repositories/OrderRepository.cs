using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameShop.Application.Interfaces;
using GameShop.Domain.Dtos.BasketDtos;
using GameShop.Domain.Dtos.OrderInfoDtos;
using GameShop.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace GameShop.Infrastructure.Repositories
{
    public class OrderRepository : BaseRepository<Order> ,IOrderRepository
    {
        public OrderRepository(ApplicationDbContext ctx)
            :base(ctx)
        {
            
        }

        public Order ScaffoldOrderForCreation(OrderInfoDto customerInfo, BasketForPaymentDto basketForPaymentDto)
        {

            var result = new Order()
            {
                OrderRef = Guid.NewGuid(),
                Name = customerInfo.Name,
                SurName = customerInfo.SurName,
                Street = customerInfo.Street,
                PostCode = customerInfo.PostCode,
                Phone = customerInfo.Phone,
                Email = customerInfo.Email,
                City = customerInfo.City,
                Country = customerInfo.Country,
                DeliveryType = customerInfo.DeliveryType,
                OrderPrice = basketForPaymentDto.BasketPrice,
                StripeRef = basketForPaymentDto.StripeRef,
                OrderStocks = basketForPaymentDto.BasketProducts.Select(x => new OrderStock()
                {
                    StockId = x.StockId,
                    Quantity = x.StockQty,
                    Price = _ctx.Stocks.Include(s => s.Product).FirstOrDefault(s => s.Id == x.StockId).Product.Price
                }).ToList()

            };

            
            return result;
        }
        
    }
}