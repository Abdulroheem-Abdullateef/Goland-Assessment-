using Golang_Assestment_Text.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly ECommerceDbContext _context;

    public OrdersController(ECommerceDbContext context)
    {
        _context = context;
    }

    // GET: api/orders
    [HttpGet]
    public async Task<IActionResult> GetUserOrders()
    {
        var email = User.FindFirstValue(ClaimTypes.Email);
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        if (user == null)
            return Unauthorized();

        var orders = await _context.Orders
            .Where(o => o.UserId == user.Id)
            .Include(o => o.OrderProducts)
            .ThenInclude(op => op.Product)
            .ToListAsync();

        return Ok(orders);
    }

    // POST: api/orders
    [HttpPost]
    public async Task<IActionResult> PlaceOrder(OrderDto orderDto)
    {
        var email = User.FindFirstValue(ClaimTypes.Email);
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        if (user == null)
            return Unauthorized();

        var order = new Order
        {
            UserId = user.Id,
            OrderDate = DateTime.UtcNow,
            Status = "Pending",
            OrderProducts = orderDto.Products.Select(p => new OrderProduct
            {
                ProductId = p.ProductId,
                Quantity = p.Quantity
            }).ToList()
        };

        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        return Ok(order);
    }

    // PUT: api/orders/{id}/status
    [Authorize(Roles = "Admin")]
    [HttpPut("{id}/status")]
    public async Task<IActionResult> UpdateOrderStatus(int id, string status)
    {
        var order = await _context.Orders.FindAsync(id);
        if (order == null)
            return NotFound("Order not found.");

        order.Status = status;
        await _context.SaveChangesAsync();
        return NoContent();
    }
}

// DTO for Order
public class OrderDto
{
    public List<OrderItemDto> Products { get; set; }
}

public class OrderItemDto
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}