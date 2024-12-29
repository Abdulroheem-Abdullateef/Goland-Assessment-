using Microsoft.EntityFrameworkCore;

public interface IOrderRepository
{
    Task<List<Order>> GetOrdersByUserIdAsync(int userId);
    Task<Order> GetOrderByIdAsync(int id);
    Task AddOrderAsync(Order order);
    Task UpdateOrderAsync(Order order);
}