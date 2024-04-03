using Stock_Market_API.Models;

namespace Stock_Market_API.Interfaces
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllAsync();
    }
}
