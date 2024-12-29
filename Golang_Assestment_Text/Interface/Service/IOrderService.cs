using Golang_Assestment_Text.Entities;

namespace Golang_Assestment_Text.Interface.Service
{
    public interface IOrderService
    {
        Task<List<Order>> GetOrdersByUserIdAsync(int userId);
        Task PlaceOrderAsync(int userId, List<OrderProduct> products);
        Task UpdateOrderStatusAsync(int orderId, string status);
    }

}