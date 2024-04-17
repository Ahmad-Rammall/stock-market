using System.ComponentModel.DataAnnotations;

namespace Stock_Market_API.DTOs.Comment
{
    public class UpdateCommentDTO
    {
        [Required]
        [MinLength(5, ErrorMessage = "Title must be 5 chars")]
        [MaxLength(200, ErrorMessage = "Title cannot be more than 200 chars")]
        public string Title { get; set; } = string.Empty;
        [Required]
        [MinLength(5, ErrorMessage = "Content must be 5 chars")]
        [MaxLength(200, ErrorMessage = "Content cannot be more than 200 chars")]
        public string Content { get; set; } = string.Empty;
    }
}
