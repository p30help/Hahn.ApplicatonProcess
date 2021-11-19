using Hahn.ApplicationProcess.July2021.Data.Common;
using Hahn.ApplicationProcess.July2021.Domain.Users;
using MediatR;

namespace Hahn.ApplicationProcess.July2021.Data.Users.Queries.GetUser
{
    /// <summary>
    /// <see cref="GetUserHandler"/>
    /// </summary>
    public class GetUserQuery : IRequest<CommandResult<UserDto>>
    {
        public int UserId { get; set; }
    }
}
