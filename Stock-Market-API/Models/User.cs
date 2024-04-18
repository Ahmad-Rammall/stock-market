using Microsoft.AspNetCore.Identity;

namespace Stock_Market_API.Models
{
    public class User : IdentityUser
    {
        public List<Portfolio> Portfolios { get; set; } = new List<Portfolio>();
    }
}
