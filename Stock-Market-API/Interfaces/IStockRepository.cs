using Stock_Market_API.DTOs.Stock;
using Stock_Market_API.Models;

namespace Stock_Market_API.Interfaces
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllAsync();
        Task<Stock?> GetByIdAsync(int id); //? because FirstOrDefault CAN BE NULL
        Task<Stock> CreateAsync(Stock stockModel);
        Task<Stock?> UpdateAsync(int id, UpdateStockRequestDTO updateDTO);
        Task<Stock?> DeleteAsync(int id);
    }
}
