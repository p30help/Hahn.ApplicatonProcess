using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.July2021.Domain.Users.DomainServices
{
    public interface IIsAssetValidated
    {
        public Task<bool> Execute(string id, string symbol, string name);
    }
}
