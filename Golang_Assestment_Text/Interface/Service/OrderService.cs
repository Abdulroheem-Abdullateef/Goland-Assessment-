namespace Golang_Assestment_Text.Interface.Service
{
    public class OrderService
    {
        private readonly OrderRepository _orderRepository;

        public OrderService(OrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<IEnumerable<Order>> GetOrdersByUserIdAsync(int userId)
        {
            return await _orderRepository.GetOrdersByUserIdAsync(userId);
        }

        public async Task<string> PlaceOrderAsync(Order order)
        {
            if (order.Products == null || order.Products.Count == 0)
            {
                return "Order must contain at least one product.";
            }

            order.Status = "Pending";
            order.OrderDate = DateTime.UtcNow;
            await _orderRepository.AddAsync(order);
            return "Order placed successfully.";
        }

        public async Task<string> CancelOrderAsync(int orderId)
        {
            var success = await _orderRepository.CancelOrderAsync(orderId);
            return success ? "Order canceled successfully." : "Failed to cancel order. Only pending orders can be canceled.";
        }

        public async Task<string> UpdateOrderStatusAsync(int orderId, string status)
        {
            var success = await _orderRepository.UpdateOrderStatusAsync(orderId, status);
            return success ? "Order status updated successfully." : "Failed to update order status.";
        }
    }
}