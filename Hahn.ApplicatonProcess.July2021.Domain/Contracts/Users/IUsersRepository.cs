using Hahn.ApplicationProcess.July2021.Domain.Users;
using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.July2021.Domain.Contracts.Users
{
    public interface IUsersRepository
    {
        Task<User> GetAsync(int id);

        Task InsertAsync(User user);

        Task DeleteAsync(User user);
    }
}
