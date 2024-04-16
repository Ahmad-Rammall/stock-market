using Stock_Market_API.DTOs.Stock;
using Stock_Market_API.Models;
using Stock_Market_API.Mappers;
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
                MarketCap = StockModel.MarketCap,
                Comments = StockModel.Comments.Select(comment => CommentMappers.ToCommentDTO(comment)).ToList()
            };
        }

        public static Stock ToStockFromCreateDTO(CreateStockRequestDTO stockDTO)
        {
            return new Stock
            {
                Symbol = stockDTO.Symbol,
                CompanyName = stockDTO.CompanyName,
                Purchase = stockDTO.Purchase,
                LastDiv = stockDTO.LastDiv,
                Industry = stockDTO.Industry,
                MarketCap = stockDTO.MarketCap
            };
        }
    }
}
