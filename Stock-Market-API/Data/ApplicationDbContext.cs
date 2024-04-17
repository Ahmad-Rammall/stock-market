using Microsoft.EntityFrameworkCore;
using Stock_Market_API.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Stock_Market_API.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options) { }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}
