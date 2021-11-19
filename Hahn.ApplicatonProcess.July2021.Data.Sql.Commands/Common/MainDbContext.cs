using Hahn.ApplicationProcess.July2021.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Hahn.ApplicationProcess.July2021.Data.Sql.Commands.Common
{
    public class MainDbContext : DbContext
    {
        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
        {
        }

        public MainDbContext()
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Asset> Assets { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(GetType().Assembly);

        }
    }
}
