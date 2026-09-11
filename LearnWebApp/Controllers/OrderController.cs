using Microsoft.AspNetCore.Mvc;

namespace LearnWebApp.Controllers
{
    [ApiController]
    [Route("orders")]
    public class OrderController : ControllerBase
    {
        private static List<Order> _orders = new List<Order>()
        {
            new Order { Id = 1, Name = "ПЕРВЫЙ" }
        };

        [HttpGet("getOrder")]
        public IActionResult GetOrder([FromQuery] int orderId)
        {
            var order = _orders.FirstOrDefault(x => x.Id == orderId);
            return Ok(order);
        }

        [HttpPost("postOrder")]
        public IActionResult CreateOrder(Order order)
        {
            _orders.Add(order);
            return Ok(order);
        }

        [HttpDelete("delOrder")]
        public IActionResult DeleteOrder([FromQuery] int orderId)
        {
            int index = _orders.FindIndex(x => x.Id == orderId);
            _orders.RemoveAt(index);
            return Ok();
        }

        [HttpPut("putOrder/{orderId}")]
        public IActionResult UpdateOrder(int orderId, Order order)
        {
            int index = _orders.FindIndex(x => x.Id == orderId);
            if (index == -1)
                return BadRequest();
            _orders[index].Name = order.Name;
            return Ok(_orders[index]);
        }
    }
    public class Order
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
