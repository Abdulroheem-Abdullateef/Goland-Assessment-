public class OrderService
{
    private readonly List<Order> _orders = new();
    private readonly ProductService _productService;

    public OrderService(ProductService productService)
    {
        _productService = productService;
    }

    public string PlaceOrder(Order order)
    {
        // Validate products
        foreach (var product in order.Products)
        {
            var existingProduct = _productService.GetProductById(product.Id);
            if (existingProduct == null)
            {
                return $"Product with ID {product.Id} does not exist.";
            }

            if (existingProduct.Stock < 1)
            {
                return $"Product {existingProduct.Name} is out of stock.";
            }

            // Reduce stock
            existingProduct.Stock -= 1;
        }

        order.OrderDate = DateTime.Now;
        order.Status = "Pending";
        _orders.Add(order);

        return "Order placed successfully.";
    }

    public IEnumerable<Order> GetOrdersForUser(int userId)
    {
        return _orders.Where(o => o.UserId == userId);
    }

    public string UpdateOrderStatus(int orderId, string status)
    {
        var order = _orders.FirstOrDefault(o => o.Id == orderId);
        if (order == null)
        {
            return "Order not found.";
        }

        order.Status = status;
        return "Order status updated successfully.";
    }

    public string CancelOrder(int orderId)
    {
        var order = _orders.FirstOrDefault(o => o.Id == orderId);
        if (order == null)
        {
            return "Order not found.";
        }

        if (order.Status != "Pending")
        {
            return "Order cannot be canceled. Only pending orders can be canceled.";
        }

        // Return stock to inventory
        foreach (var product in order.Products)
        {
            var existingProduct = _productService.GetProductById(product.Id);
            if (existingProduct != null)
            {
                existingProduct.Stock += 1;
            }
        }

        order.Status = "Canceled";
        return "Order canceled successfully.";
    }
}