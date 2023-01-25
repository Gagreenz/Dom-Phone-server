using Microsoft.EntityFrameworkCore;
using Dom_Phone_server.Models.Account;

namespace Dom_Phone_server.Models.DB
{
    public class UserContext : DbContext
    {
        public DbSet<User> Users => Set<User>();
        public UserContext() => Database.EnsureCreated();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlite("Data Source=helloapp.db");
        }
    }
}
