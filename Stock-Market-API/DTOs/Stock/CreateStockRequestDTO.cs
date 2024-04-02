using System.ComponentModel.DataAnnotations.Schema;

namespace Stock_Market_API.DTOs.Stock
{
    // We Don't Need ID + Comments to Add Stock
    public class CreateStockRequestDTO
    {
        public string Symbol { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public decimal Purchase { get; set; }
        public decimal LastDiv { get; set; }
        public string Industry { get; set; } = string.Empty;
        public long MarketCap { get; set; }
    }
}
