using System.Collections.Generic;
using Hahn.ApplicationProcess.July2021.Data.Common;
using MediatR;

namespace Hahn.ApplicationProcess.July2021.Data.Users.Commands.EditUser
{
    /// <summary>
    /// <see cref="EditUserCommandHandler"/>
    /// </summary>
    public class EditUserCommand : IRequest<CommandResult>
    {
        public int Id { get; set; }

        public int Age { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Street { get; set; }

        public int PostalCode { get; set; }

        public string HouseNumber { get; set; }

        public string Email { get; set; }

        public List<EditAssetDto> Assets { get; set; }
    }

    public class EditAssetDto
    {
        public string Key { get; set; }

        public string Symbol { get; set; }

        public string Name { get; set; }

    }
}
