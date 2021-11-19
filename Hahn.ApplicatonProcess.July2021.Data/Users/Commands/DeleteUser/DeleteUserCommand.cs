using Hahn.ApplicationProcess.July2021.Data.Common;
using MediatR;
using System;

namespace Hahn.ApplicationProcess.July2021.Data.Users.Commands.DeleteUser
{
    /// <summary>
    /// <see cref="DeleteUserCommandHandler"/>
    /// </summary>
    public class DeleteUserCommand : IRequest<CommandResult>
    {
        public int UserId { get; set; }
    }
}
