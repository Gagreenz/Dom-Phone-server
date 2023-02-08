using Dom_Phone_server.Models.Data;
using Microsoft.EntityFrameworkCore;

namespace Dom_Phone_server.Models.DB
{
    public class UserContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<RefreshToken> RefreshTokens { get; set; } = null!;
        public UserContext(DbContextOptions<UserContext> options) 
            : base(options)
        {
            Database.EnsureCreated();
        }

    }
}

