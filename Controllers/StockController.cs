using Microsoft.AspNetCore.Mvc;
using SPCManagementSystemAPI.Models;
using SPCManagementSystemAPI.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SPCManagementSystemAPI.Controllers
{
    [Route("api/stocks")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IStockRepository _stockRepository;

        public StockController(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Stock>>> GetAllStock()
        {
            return Ok(await _stockRepository.GetAllStock());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Stock>> GetStockById(string id)
        {
            var stock = await _stockRepository.GetStockById(id);
            if (stock == null)
                return NotFound("Stock not found.");
            return Ok(stock);
        }

        [HttpPost]
        public async Task<ActionResult<Stock>> AddStock(Stock stock)
        {
            var createdStock = await _stockRepository.AddStock(stock);
            return CreatedAtAction(nameof(GetStockById), new { id = createdStock.Id }, createdStock);
        }
    }
}
