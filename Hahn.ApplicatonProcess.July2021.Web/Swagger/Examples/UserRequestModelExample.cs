using Hahn.ApplicationProcess.July2021.Web.Integration.Users.Requests;
using Swashbuckle.AspNetCore.Filters;
using System.Collections.Generic;

namespace Hahn.ApplicationProcess.July2021.Web.Swagger.Examples
{
    public class UserRequestModelExample : IExamplesProvider<UserRequest>
    {
        public UserRequest GetExamples()
        {
            return new UserRequest
            {
                street = "Street 27",
                age = 32,
                email = "test@gmail.com",
                first_name = "mahdi",
                last_name = "radi",
                house_number = "24",
                postal_code = 12345,
                assets = new List<AssetRequest>()
                {
                    new AssetRequest()
                    {
                        key = "bitcoin",
                        name = "Bitcoin",
                        symbol = "BTC"
                    }
                }
            };
        }
    }
}
