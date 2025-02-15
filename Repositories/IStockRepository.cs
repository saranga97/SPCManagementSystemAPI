using SPCManagementSystemAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SPCManagementSystemAPI.Repositories
{
    public interface IStockRepository
    {
        Task<IEnumerable<Stock>> GetAllStock();
        Task<Stock> GetStockById(string id);
        Task<Stock> AddStock(Stock stock);
        Task<bool> UpdateStock(Stock stock);
        Task<bool> DeleteStock(string id); 
    }
}
