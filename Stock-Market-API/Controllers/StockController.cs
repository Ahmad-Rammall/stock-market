using Microsoft.AspNetCore.Mvc;
using Stock_Market_API.Data;
using Stock_Market_API.Mappers;
using Stock_Market_API.DTOs.Stock;
using Stock_Market_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Stock_Market_API.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public StockController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStocks()
        {
            var stocks = await _context.Stocks.ToListAsync();
            var stockDto = stocks.Select(s => StockMappers.ToStockDTO(s));

            return Ok(stockDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStockById(int id)
        {
            var stock = await _context.Stocks.FindAsync(id);
            if (stock == null) return NotFound();

            return Ok(stock);
        }

        [HttpPost]
        public async Task<IActionResult> CreateStock([FromBody] CreateStockRequestDTO stockDTO)
        {
            var stockModel = StockMappers.ToStockFromCreateDTO(stockDTO);
            await _context.Stocks.AddAsync(stockModel);
            await _context.SaveChangesAsync();

            // execute GetStockById with the created stock ID then return in the form of ToStockDTO()
            return CreatedAtAction(nameof(GetStockById), new { id = stockModel.Id }, StockMappers.ToStockDTO(stockModel));
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateStock([FromRoute] int id, [FromBody] UpdateStockRequestDTO updateDTO)
        {
            var stockModel = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);
            if (stockModel == null) return NotFound();

            stockModel.Symbol = updateDTO.Symbol;
            stockModel.CompanyName = updateDTO.CompanyName;
            stockModel.MarketCap = updateDTO.MarketCap;
            stockModel.Purchase = updateDTO.Purchase;
            stockModel.LastDiv = updateDTO.LastDiv;
            stockModel.Industry = updateDTO.Industry;

            await _context.SaveChangesAsync();

            return Ok(StockMappers.ToStockDTO(stockModel));
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteStock([FromRoute] int id)
        {
            var stockModel = _context.Stocks.FirstOrDefault(x => x.Id == id);
            if (stockModel == null) return NotFound();

            _context.Stocks.Remove(stockModel);
            await _context.SaveChangesAsync();

            //return NoContent();
            return Ok("Stock Deleted");
        }
    }
}
