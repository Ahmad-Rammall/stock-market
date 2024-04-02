﻿using Microsoft.AspNetCore.Mvc;
using Stock_Market_API.Data;
using Stock_Market_API.Mappers;

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
    }
}
