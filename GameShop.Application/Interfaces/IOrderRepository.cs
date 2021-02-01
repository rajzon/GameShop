using GameShop.Domain.Dtos.BasketDtos;
using GameShop.Domain.Dtos.OrderInfoDtos;
using GameShop.Domain.Model;

namespace GameShop.Application.Interfaces
{
    public interface IOrderRepository : IBaseRepository<Order>
    {
        public Order ScaffoldOrderForCreation(OrderInfoDto customerInfo, BasketForPaymentDto basketForPaymentDto);
    }
}