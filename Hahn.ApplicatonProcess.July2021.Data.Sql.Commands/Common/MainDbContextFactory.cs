using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Hahn.ApplicationProcess.July2021.Data.Sql.Commands.Common
{
    public class MainDbContextFactory : IDesignTimeDbContextFactory<MainDbContext>
    {
        public MainDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<MainDbContext>();
            builder.UseSqlServer("Data Source=.;Initial Catalog=HahnDbDev;Trusted_Connection=True;MultipleActiveResultSets=True;");
            return new MainDbContext(builder.Options);
        }
    }
}