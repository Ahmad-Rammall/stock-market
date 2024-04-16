namespace Stock_Market_API.DTOs.Comment
{
    public class CommentDTO
    {
        public int Id { get; set; }
        public int StockId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
