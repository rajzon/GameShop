using GameShop.Domain.Dtos.BasketDtos;
using GameShop.Domain.Dtos.CustomerDto;
using GameShop.Domain.Model;

namespace GameShop.Application.Interfaces
{
    public interface IOrderRepository : IBaseRepository<Order>
    {
        public Order ScaffoldOrderForCreation(CustomerInfoDto customerInfo, BasketForPaymentDto basketForPaymentDto);
    }
}