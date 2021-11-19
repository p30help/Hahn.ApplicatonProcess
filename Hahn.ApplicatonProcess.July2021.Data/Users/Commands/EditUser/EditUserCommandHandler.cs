using Hahn.ApplicationProcess.July2021.Data.Common;
using Hahn.ApplicationProcess.July2021.Domain.Contracts;
using Hahn.ApplicationProcess.July2021.Domain.Users.DomainServices;
using Hahn.ApplicationProcess.July2021.Domain.ValueObjects;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Hahn.ApplicationProcess.July2021.Data.Common.Events;
using Hahn.ApplicationProcess.July2021.Domain.Exceptions;

namespace Hahn.ApplicationProcess.July2021.Data.Users.Commands.EditUser
{
    public class EditUserCommandHandler : IRequestHandler<EditUserCommand, CommandResult>
    {
        private readonly IHahnUnitOfWork _unitOfWork;
        private readonly IEventDispatcher _eventDispatcher;
        private readonly IIsAssetValidated _isAssetIdValidated;
        private readonly IIdGenerator _idGenerator;

        public EditUserCommandHandler(IHahnUnitOfWork unitOfWork, IEventDispatcher eventDispatcher, IIsAssetValidated isAssetIdValidated, IIdGenerator idGenerator)
        {
            _unitOfWork = unitOfWork;
            _eventDispatcher = eventDispatcher;
            _isAssetIdValidated = isAssetIdValidated;
            _idGenerator = idGenerator;
        }

        public async Task<CommandResult> Handle(EditUserCommand request, CancellationToken cancellationToken)
        {

            var user = await _unitOfWork.UsersRepository.GetAsync(request.Id);

            if (user == null)
            {
                throw new InvalidEntityStateException("User not found");
            }

            user.SetAge(new HumanAge(request.Age));
            user.SetAddress(request.Street, request.HouseNumber, request.PostalCode);
            user.SetEmail(new EmailField(request.Email));
            user.SetFirstName(new HumanName(request.FirstName));
            user.SetLastName(new HumanName(request.LastName));

            user.ClearAssets();
            if (request.Assets != null)
            {
                foreach (var asset in request.Assets)
                {
                    var newAssetId = await _idGenerator.GenerateAsync();
                    await user.AddAsset(newAssetId, asset.Key, asset.Symbol, asset.Name, _isAssetIdValidated);
                }
            }

            await _unitOfWork.CommitAsync();

            await _eventDispatcher.PublishDomainEventsAsync(user.GetEvents());

            return CommandResult.Ok();
        }
    }
}
