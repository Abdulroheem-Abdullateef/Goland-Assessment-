

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly OrderService _orderService;

        public OrdersController(OrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public IActionResult PlaceOrder([FromBody] Order order)
        {
            var result = _orderService.PlaceOrder(order);
            if (result.Contains("does not exist") || result.Contains("out of stock"))
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet("{userId}")]
        public IActionResult GetOrders(int userId)
        {
            var orders = _orderService.GetOrdersForUser(userId);
            if (!orders.Any())
            {
                return NotFound("No orders found for the user.");
            }
            return Ok(orders);
        }

        [HttpPut("{orderId}/status")]
        public IActionResult UpdateOrderStatus(int orderId, [FromQuery] string status)
        {
            var result = _orderService.UpdateOrderStatus(orderId, status);
            if (result == "Order not found.")
            {
                return NotFound(result);
            }
            return Ok(result);
        }

        [HttpDelete("{orderId}")]
        public IActionResult CancelOrder(int orderId)
        {
            var result = _orderService.CancelOrder(orderId);
            if (result == "Order not found." || result == "Order cannot be canceled. Only pending orders can be canceled.")
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }

