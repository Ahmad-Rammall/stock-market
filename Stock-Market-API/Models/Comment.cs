namespace Stock_Market_API.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int StockId { get; set; }

        // Navigation Property
        public Stock Stock { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
