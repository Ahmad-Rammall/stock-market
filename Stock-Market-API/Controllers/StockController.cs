using Microsoft.AspNetCore.Mvc;
using Stock_Market_API.Data;
using Stock_Market_API.Mappers;
using Stock_Market_API.DTOs.Stock;

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
        public IActionResult GetAllStocks()
        {
            var stocks = _context.Stocks.ToList().Select(s => StockMappers.ToStockDTO(s));
            return Ok(stocks);
        }

        [HttpGet("{id}")]
        public IActionResult GetStockById(int id)
        {
            var stock = _context.Stocks.Find(id);
            if(stock == null) return NotFound();

            return Ok(stock);
        }

        [HttpPost]
        public IActionResult CreateStock([FromBody] CreateStockRequestDTO stockDTO)
        {
            var stockModel = StockMappers.ToStockFromCreateDTO(stockDTO);
            _context.Stocks.Add(stockModel);
            _context.SaveChanges();

            // execute GetStockById with the created stock ID then return in the form of ToStockDTO()
            return CreatedAtAction(nameof(GetStockById), new { id = stockModel.Id }, StockMappers.ToStockDTO(stockModel));
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult UpdateStock([FromRoute] int id, [FromBody] UpdateStockRequestDTO updateDTO)
        {
            var stockModel = _context.Stocks.FirstOrDefault(x => x.Id == id);
            if(stockModel == null) return NotFound();

            stockModel.Symbol = updateDTO.Symbol;
            stockModel.CompanyName = updateDTO.CompanyName;
            stockModel.MarketCap = updateDTO.MarketCap;
            stockModel.Purchase = updateDTO.Purchase;
            stockModel.LastDiv = updateDTO.LastDiv;
            stockModel.Industry = updateDTO.Industry;

            _context.SaveChanges();

            return Ok(StockMappers.ToStockDTO(stockModel));
        }
    }
}
