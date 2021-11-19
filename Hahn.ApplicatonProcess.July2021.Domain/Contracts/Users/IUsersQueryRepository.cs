using Hahn.ApplicationProcess.July2021.Domain.Users;
using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.July2021.Domain.Contracts.Users
{
    public interface IUsersReadonlyRepository
    {
        Task<UserDto> GetAsync(int id);
    }
}