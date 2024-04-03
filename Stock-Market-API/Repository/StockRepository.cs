using Microsoft.EntityFrameworkCore;
using Stock_Market_API.Data;
using Stock_Market_API.Interfaces;
using Stock_Market_API.Models;

namespace Stock_Market_API.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDbContext _context;
        public StockRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public Task<List<Stock>> GetAllAsync()
        {
            return _context.Stocks.ToListAsync();
        }
    }
}
