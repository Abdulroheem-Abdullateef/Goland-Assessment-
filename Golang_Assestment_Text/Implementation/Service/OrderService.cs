using Golang_Assestment_Text.Entities;
using Golang_Assestment_Text.Interface.Service;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;

    public OrderService(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public Task<List<Order>> GetOrdersByUserIdAsync(int userId) => _orderRepository.GetOrdersByUserIdAsync(userId);

    public async Task PlaceOrderAsync(int userId, List<OrderProduct> products)
    {
        var order = new Order
        {
            UserId = userId,
            OrderDate = DateTime.UtcNow,
            Status = "Pending",
            OrderProducts = products.Select(p => new OrderProduct
            {
                ProductId = p.ProductId,
                Quantity = p.Quantity
            }).ToList()
        };

        await _orderRepository.AddOrderAsync(order);
    }

    public async Task UpdateOrderStatusAsync(int orderId, string status)
    {
        var order = await _orderRepository.GetOrderByIdAsync(orderId);
        if (order == null)
            throw new ArgumentException("Order not found.");

        order.Status = status;
        await _orderRepository.UpdateOrderAsync(order);
    }
}