using Microsoft.AspNetCore.Mvc;
using SPCManagementSystemAPI.Models;
using SPCManagementSystemAPI.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SPCManagementSystemAPI.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;

        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetAllOrders()
        {
            return Ok(await _orderRepository.GetAllOrders());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrderById(string id)
        {
            var order = await _orderRepository.GetOrderById(id);
            if (order == null)
                return NotFound("Order not found.");
            return Ok(order);
        }

        [HttpPost]
        public async Task<ActionResult<Order>> PlaceOrder(Order order)
        {
            var createdOrder = await _orderRepository.PlaceOrder(order);
            return CreatedAtAction(nameof(GetOrderById), new { id = createdOrder.Id }, createdOrder);
        }
    }
}
