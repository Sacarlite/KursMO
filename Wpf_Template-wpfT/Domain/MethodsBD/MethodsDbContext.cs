using Domain.UserBd;
using Microsoft.EntityFrameworkCore;

namespace Domain.MethodsBD
{
   
    public class MethodsDbContext : DbContext
    {
        public MethodsDbContext() : base()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source= method.db");
        }
        public DbSet<Method> Users { get; set; }
    }
}