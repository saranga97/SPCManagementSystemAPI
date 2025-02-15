using Microsoft.EntityFrameworkCore;
using SPCManagementSystemAPI.Data;
using SPCManagementSystemAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPCManagementSystemAPI.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly SPCContext _context;

        public OrderRepository(SPCContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Order>> GetAllOrders()
        {
            return await _context.Orders.ToListAsync();
        }

        public async Task<Order> GetOrderById(string id)
        {
            return await _context.Orders.FindAsync(id);
        }

        public async Task<Order> PlaceOrder(Order order)
        {
            // Auto-generate a unique Order ID (ORD001, ORD002, etc.)
            string newOrderId = GenerateNewOrderId();
            order.Id = newOrderId; // Assign the generated Order ID

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }

        private string GenerateNewOrderId()
        {
            var lastOrder = _context.Orders.OrderByDescending(o => o.Id).FirstOrDefault();
            if (lastOrder == null)
            {
                return "ORD001"; // First ID
            }

            string lastId = lastOrder.Id.Substring(3); // Remove 'ORD'
            int newId = int.Parse(lastId) + 1; // Increment number
            return $"ORD{newId:D3}"; // Format as ORD001, ORD002, etc.
        }
    }
}

