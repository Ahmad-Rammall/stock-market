using Microsoft.AspNetCore.Mvc;
using Stock_Market_API.DTOs.Comment;
using Stock_Market_API.Interfaces;
using Stock_Market_API.Mappers;
using System.Runtime.InteropServices;

namespace Stock_Market_API.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepo;
        private readonly IStockRepository _stockRepo;
        public CommentController(ICommentRepository commentRepo, IStockRepository stockRepo)
        {
            _commentRepo = commentRepo;
            _stockRepo = stockRepo;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var comments = await _commentRepo.GetAllAsync();
            var commentDTO = comments.Select(s => CommentMappers.ToCommentDTO(s));

            return Ok(commentDTO);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCommentById(int id)
        {
            var comment = await _commentRepo.GetByIdAsync(id);
            if (comment == null) { return NotFound(); }

            return Ok(CommentMappers.ToCommentDTO(comment));
        }
        [HttpPost("{stockId:int}")]
        public async Task<IActionResult> CreateComment([FromRoute] int stockId, CreateCommentDTO commentDTO)
        {
            // ModelState Validation
            if(!ModelState.IsValid) { return BadRequest(ModelState); }
            
            if (!await _stockRepo.StockExists(stockId)) { return BadRequest("Stock Doesnt Exist!"); }

            var commentModel = CommentMappers.ToCommentFromCreateDTO(commentDTO, stockId);
            await _commentRepo.CreateCommentAsync(commentModel);

            return Ok("DONE");
        }
        [HttpPut("{commentId:int}")]
        public async Task<IActionResult> Updatecomment([FromRoute]int commentId, [FromBody] UpdateCommentDTO commentDTO)
        {
            // ModelState Validation
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            var comment = await _commentRepo.UpdateCommentAsync(commentId, CommentMappers.ToCommentFromUpdateDTO(commentDTO));
            if(comment == null) return NotFound();


            return Ok(CommentMappers.ToCommentDTO(comment));
        }
        [HttpDelete("{commentId:int}")]
        public async Task<IActionResult> DeleteComment([FromRoute] int commentId)
        {
            var comment = await _commentRepo.DeleteCommentAsync(commentId);
            if (comment == null) return BadRequest("Comment Not Found!");

            return Ok(comment);
        }
    }
}
