using Stock_Market_API.DTOs.Stock;
using Stock_Market_API.Models;
using System.Runtime.CompilerServices;

namespace Stock_Market_API.Mappers
{
    public class StockMappers
    {
        public static StockDTO ToStockDTO(Stock StockModel)
        {
            return new StockDTO
            {
                Id = StockModel.Id,
                Symbol = StockModel.Symbol,
                CompanyName = StockModel.CompanyName,
                Purchase = StockModel.Purchase,
                LastDiv = StockModel.LastDiv,
                Industry = StockModel.Industry,
                MarketCap = StockModel.MarketCap
            };

        }
    }
}
