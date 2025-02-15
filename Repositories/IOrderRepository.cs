using SPCManagementSystemAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SPCManagementSystemAPI.Repositories
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAllOrders();
        Task<Order> GetOrderById(string id);  
        Task<Order> PlaceOrder(Order order);
    }
}

