using Hahn.ApplicationProcess.July2021.Web.Integration.Users.Requests;
using System.Linq;
using Hahn.ApplicationProcess.July2021.Data.Users.Commands.AddUser;
using Hahn.ApplicationProcess.July2021.Data.Users.Commands.EditUser;
using Hahn.ApplicationProcess.July2021.Domain.Users;
using Hahn.ApplicationProcess.July2021.Web.Integration.Users.Responses;

namespace Hahn.ApplicationProcess.July2021.Web.Tools
{
    public interface IMapper
    {
        AddUserCommand ToAddUserCommand(UserRequest request);

        EditUserCommand ToEditUserCommand(UserRequest request);

        UserResponse ToUserResponse(UserDto dataData);
    }
    public class Mapper : IMapper
    {
        public AddUserCommand ToAddUserCommand(UserRequest request)
        {
            var model = new AddUserCommand()
            {
                FirstName = request.first_name,
                LastName = request.last_name,
                HouseNumber = request.house_number,
                Street = request.street,
                PostalCode = request.postal_code,
                Email = request.email,
                Age = request.age,
                Assets = request.assets?.Select(z => new AddAssetDto
                {
                    Key = z.key,
                    Name = z.name,
                    Symbol = z.symbol
                }).ToList(),
            };

            return model;
        }

        public EditUserCommand ToEditUserCommand(UserRequest request)
        {
            var model = new EditUserCommand()
            {
                FirstName = request.first_name,
                LastName = request.last_name,
                HouseNumber = request.house_number,
                Street = request.street,
                PostalCode = request.postal_code,
                Email = request.email,
                Age = request.age,
                Assets = request.assets?.Select(z => new EditAssetDto
                {
                    Key = z.key,
                    Name = z.name,
                    Symbol = z.symbol
                }).ToList(),
            };

            return model;
        }

        public UserResponse ToUserResponse(UserDto request)
        {
            var model = new UserResponse()
            {
                id = request.Id,
                first_name = request.FirstName,
                last_name = request.LastName,
                address = request.Address,
                email = request.Email,
                age = request.Age,
                assets = request.Assets?.Select(z => new AssetResponse
                {
                    key = z.Key,
                    name = z.Name,
                    symbol = z.Symbol
                }).ToList(),
            };

            return model;
        }
    }
}
