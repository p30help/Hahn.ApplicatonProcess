using Hahn.ApplicationProcess.July2021.Data.Sql.Queries.Common;
using Hahn.ApplicationProcess.July2021.Domain.Contracts.Users;
using Hahn.ApplicationProcess.July2021.Domain.Users;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.July2021.Data.Sql.Queries.Users
{
    public class UsersReadonlyRepository : IUsersReadonlyRepository
    {
        private readonly QueryDbContext _context;

        public UsersReadonlyRepository(QueryDbContext context)
        {
            _context = context;
        }

        public Task<UserDto> GetAsync(int id)
        {
            return _context.Users
                .Include(x => x.Assets)
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == id);
        }

    }
}