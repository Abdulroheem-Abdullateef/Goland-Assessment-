using Microsoft.EntityFrameworkCore;

public class OrderRepository : Repository<Order>
{
    private readonly ECommerceDbContext _context;

    public OrderRepository(ECommerceDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Order>> GetOrdersByUserIdAsync(int userId)
    {
        return await _context.Orders
            .Include(o => o.Products)
            .Where(o => o.UserId == userId)
            .ToListAsync();
    }

    public async Task<bool> CancelOrderAsync(int orderId)
    {
        var order = await _context.Orders.FindAsync(orderId);
        if (order != null && order.Status == "Pending")
        {
            order.Status = "Canceled";
            await _context.SaveChangesAsync();
            return true;
        }

        return false;
    }


    public async Task<bool> UpdateOrderStatusAsync(int orderId, string status)
    {
        var order = await _context.Orders.FindAsync(orderId);
        if (order != null)
        {
            order.Status = status;
            await _context.SaveChangesAsync();
            return true;
        }

        return false;
    }
}