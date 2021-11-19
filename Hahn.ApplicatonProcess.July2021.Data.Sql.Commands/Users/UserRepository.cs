using Hahn.ApplicationProcess.July2021.Data.Sql.Commands.Common;
using Hahn.ApplicationProcess.July2021.Domain.Contracts.Users;
using Hahn.ApplicationProcess.July2021.Domain.Users;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.July2021.Data.Sql.Commands.Users
{
    public class UsersRepository : IUsersRepository
    {
        private readonly MainDbContext _context;

        public UsersRepository(MainDbContext context)
        {
            _context = context;
        }

        public Task<User> GetAsync(int id)
        {
            return _context.Users
                .Include(x => x.Assets)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task InsertAsync(User user)
        {
            await _context.Users.AddAsync(user);
        }

        public async Task DeleteAsync(User user)
        {
            var item = await _context.Users
                .Include(x => x.Assets)
                .SingleOrDefaultAsync(x => x.Id == user.Id);

            _context.Users.Remove(item);
        }
    }
}