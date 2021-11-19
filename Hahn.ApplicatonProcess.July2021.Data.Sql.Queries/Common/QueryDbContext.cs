using Hahn.ApplicationProcess.July2021.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Hahn.ApplicationProcess.July2021.Data.Sql.Queries.Common
{
    public class QueryDbContext : DbContext
    {
        public QueryDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<UserDto> Users { get; set; }

        public DbSet<AssetDto> Assets { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //
            builder.ApplyConfigurationsFromAssembly(GetType().Assembly);

            base.OnModelCreating(builder);
        }
    }
}
