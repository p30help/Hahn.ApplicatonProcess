using System.Threading;
using System.Threading.Tasks;
using Hahn.ApplicationProcess.July2021.Data.Common;
using Hahn.ApplicationProcess.July2021.Domain.Contracts;
using Hahn.ApplicationProcess.July2021.Domain.Exceptions;
using MediatR;

namespace Hahn.ApplicationProcess.July2021.Data.Users.Commands.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, CommandResult>
    {
        private readonly IHahnUnitOfWork _unitOfWork;

        public DeleteUserCommandHandler(IHahnUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CommandResult> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UsersRepository.GetAsync(request.UserId);

            if (user == null)
            {
                throw new InvalidEntityStateException("User not found");
            }

            user.Remove();

            await _unitOfWork.UsersRepository.DeleteAsync(user);
            await _unitOfWork.CommitAsync();

            return CommandResult.Ok();
        }
    }
}
