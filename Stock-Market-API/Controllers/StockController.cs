using Microsoft.AspNetCore.Mvc;
using Stock_Market_API.Data;
using Stock_Market_API.Mappers;
using Stock_Market_API.DTOs.Stock;
using Stock_Market_API.Models;
using Microsoft.EntityFrameworkCore;
using Stock_Market_API.Interfaces;

namespace Stock_Market_API.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IStockRepository _stockRepo;

        public StockController(ApplicationDbContext context, IStockRepository stockRepo)
        {
            _stockRepo = stockRepo;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStocks()
        {
            var stocks = await _stockRepo.GetAllAsync();
            var stockDto = stocks.Select(s => StockMappers.ToStockDTO(s));

            return Ok(stockDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetStockById(int id)
        {
            var stock = await _stockRepo.GetByIdAsync(id);
            if (stock == null) return NotFound();

            return Ok(stock);
        }

        [HttpPost]
        public async Task<IActionResult> CreateStock([FromBody] CreateStockRequestDTO stockDTO)
        {
            var stockModel = StockMappers.ToStockFromCreateDTO(stockDTO);
            await _stockRepo.CreateAsync(stockModel);

            // execute GetStockById with the created stock ID then return in the form of ToStockDTO()
            return CreatedAtAction(nameof(GetStockById), new { id = stockModel.Id }, StockMappers.ToStockDTO(stockModel));
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateStock([FromRoute] int id, [FromBody] UpdateStockRequestDTO updateDTO)
        {
            var stockModel = await _stockRepo.UpdateAsync(id, updateDTO);
            if (stockModel == null) return NotFound();

            return Ok(StockMappers.ToStockDTO(stockModel));
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteStock([FromRoute] int id)
        {
            var stockModel = await _stockRepo.DeleteAsync(id);
            if (stockModel == null) return NotFound();

            //return NoContent();
            return Ok("Stock Deleted");
        }
    }
}
