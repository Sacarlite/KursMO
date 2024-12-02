using Microsoft.EntityFrameworkCore;

namespace Domain.MethodsBD
{
    public class MethodsDbContext : DbContext, IDisposable
    {
        public MethodsDbContext()
            : base()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source= method.db");
        }

        public DbSet<Method> Methods { get; set; }
        public DbSet<Сlassification> Сlasses { get; set; }

        public void Dispose() { }
    }
}
