using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Hahn.ApplicationProcess.July2021.Data.Users.Services
{
    public class GetCoincapService
    {
        private readonly IHttpClientFactory _clientFactory;

        public GetCoincapService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<List<CoincapModel>> GetListAsync()
        {
            var client = _clientFactory.CreateClient("coincap");

            var dataStr = await client.GetStringAsync("https://api.coincap.io/v2/assets");
            var list = JsonConvert.DeserializeObject<CoincapModelRoot>(dataStr);

            return list?.data;
        }
    }

    public class CoincapModel
    {
        public  string id { get; set; }

        public string symbol { get; set; }

        public string name { get; set; }
    }

    public class CoincapModelRoot
    {
        public List<CoincapModel> data { get; set; }
    }
}
