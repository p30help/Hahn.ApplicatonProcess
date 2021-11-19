using Hahn.ApplicationProcess.July2021.Domain.Users.DomainServices;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.July2021.Data.Users.Services
{
    public class IsAssetValidated : IIsAssetValidated
    {
        private readonly IHttpClientFactory _clientFactory;

        public IsAssetValidated(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<bool> Execute(string id, string symbol, string name)
        {
            var getCoincapService = new GetCoincapService(_clientFactory);
            var coins = await getCoincapService.GetListAsync();

            var existed = coins.Any(x =>
                x.id.Equals(id, StringComparison.OrdinalIgnoreCase) &&
                x.name.Equals(name, StringComparison.OrdinalIgnoreCase) &&
                x.symbol.Equals(symbol, StringComparison.OrdinalIgnoreCase)
                );

            return existed;
        }
    }
}
