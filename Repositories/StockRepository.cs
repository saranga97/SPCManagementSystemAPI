using Microsoft.EntityFrameworkCore;
using SPCManagementSystemAPI.Data;
using SPCManagementSystemAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPCManagementSystemAPI.Repositories
{
    public class StockRepository : IStockRepository
    {
        private readonly SPCContext _context;

        public StockRepository(SPCContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Stock>> GetAllStock()
        {
            return await _context.Stocks.ToListAsync();
        }

        public async Task<Stock> GetStockById(string id)
        {
            return await _context.Stocks.FindAsync(id);
        }

        public async Task<Stock> AddStock(Stock stock)
        {
            // Generate a unique Stock ID (STK001, STK002, etc.)
            string newStockId = GenerateNewStockId();
            stock.Id = newStockId;

            _context.Stocks.Add(stock);
            await _context.SaveChangesAsync();
            return stock;
        }

        public async Task<bool> UpdateStock(Stock stock)
        {
            _context.Stocks.Update(stock);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteStock(string id)
        {
            var stock = await _context.Stocks.FindAsync(id);
            if (stock == null) return false;
            _context.Stocks.Remove(stock);
            return await _context.SaveChangesAsync() > 0;
        }

        private string GenerateNewStockId()
        {
            var lastStock = _context.Stocks.OrderByDescending(s => s.Id).FirstOrDefault();
            if (lastStock == null)
            {
                return "STK001"; // First ID
            }

            string lastId = lastStock.Id.Substring(3);
            int newId = int.Parse(lastId) + 1;
            return $"STK{newId:D3}";
        }
    }
}
