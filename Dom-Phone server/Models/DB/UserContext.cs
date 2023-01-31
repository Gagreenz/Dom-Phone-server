using Microsoft.EntityFrameworkCore;

namespace Dom_Phone_server.Models.DB
{
    public class UserContext : DbContext
    {
        public DbSet<User> Users => Set<User>();
        public UserContext(DbContextOptions<UserContext> options) 
            : base(options)
        {
            Database.EnsureCreated();
        }

    }
}

