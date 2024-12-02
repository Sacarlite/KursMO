using Microsoft.EntityFrameworkCore;

namespace Domain.UserBd
{
    public class UserDbContext : DbContext
    {
        public UserDbContext()
            : base()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source= Users.db");
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
    }
}
