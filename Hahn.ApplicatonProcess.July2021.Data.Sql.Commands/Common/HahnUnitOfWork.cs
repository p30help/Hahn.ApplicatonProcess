using Hahn.ApplicationProcess.July2021.Data.Sql.Commands.Users;
using Hahn.ApplicationProcess.July2021.Domain.Contracts;
using Hahn.ApplicationProcess.July2021.Domain.Contracts.Users;
using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.July2021.Data.Sql.Commands.Common
{
    public class HahnUnitOfWork : IHahnUnitOfWork
    {
        private readonly MainDbContext _context;

        public HahnUnitOfWork(MainDbContext dbContext)
        {
            _context = dbContext;
            UsersRepository = new UsersRepository(dbContext);
        }

        public IUsersRepository UsersRepository { get; set; }

        public Task<int> CommitAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}

