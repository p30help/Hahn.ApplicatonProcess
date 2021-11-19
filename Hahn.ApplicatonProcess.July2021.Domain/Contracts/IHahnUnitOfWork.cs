using System.Threading.Tasks;
using Hahn.ApplicationProcess.July2021.Domain.Contracts.Users;

namespace Hahn.ApplicationProcess.July2021.Domain.Contracts
{
    public interface IHahnUnitOfWork 
    {
        IUsersRepository UsersRepository { get; set; }

        Task<int> CommitAsync();
    }
}