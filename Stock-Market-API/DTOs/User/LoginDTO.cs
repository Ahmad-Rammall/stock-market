using System.ComponentModel.DataAnnotations;

namespace Stock_Market_API.DTOs.User
{
    public class LoginDTO
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
