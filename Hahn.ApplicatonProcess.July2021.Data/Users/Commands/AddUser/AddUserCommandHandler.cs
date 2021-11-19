using System.Threading;
using System.Threading.Tasks;
using Hahn.ApplicationProcess.July2021.Data.Common;
using Hahn.ApplicationProcess.July2021.Data.Common.Events;
using Hahn.ApplicationProcess.July2021.Domain.Contracts;
using Hahn.ApplicationProcess.July2021.Domain.Users;
using Hahn.ApplicationProcess.July2021.Domain.Users.DomainServices;
using Hahn.ApplicationProcess.July2021.Domain.ValueObjects;
using MediatR;

namespace Hahn.ApplicationProcess.July2021.Data.Users.Commands.AddUser
{
    public class AddUserCommandHandler : IRequestHandler<AddUserCommand, CommandResult<int>>
    {
        private readonly IHahnUnitOfWork _unitOfWork;
        private readonly IIdGenerator _idGenerator;
        private readonly IEventDispatcher _eventDispatcher;
        private readonly IIsAssetValidated _isAssetIdValidated;

        public AddUserCommandHandler(IHahnUnitOfWork unitOfWork, IEventDispatcher eventDispatcher, IIdGenerator idGenerator, IIsAssetValidated isAssetIdValidated)
        {
            _unitOfWork = unitOfWork;
            _eventDispatcher = eventDispatcher;
            _idGenerator = idGenerator;
            _isAssetIdValidated = isAssetIdValidated;
        }

        public async Task<CommandResult<int>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            var newId = await _idGenerator.GenerateAsync();

            var user = User.Create(newId , 
                new HumanName(request.FirstName), 
                new HumanName(request.LastName), 
                new HumanAge(request.Age),
                new EmailField(request.Email),
                request.Street, request.HouseNumber, request.PostalCode);

            if (request.Assets != null)
            {
                foreach (var asset in request.Assets)
                {
                    var newAssetId = await _idGenerator.GenerateAsync();

                    await user.AddAsset(newAssetId, asset.Key, asset.Symbol, asset.Name, _isAssetIdValidated);
                }
            }

            await _unitOfWork.UsersRepository.InsertAsync(user);
            await _unitOfWork.CommitAsync();

             await _eventDispatcher.PublishDomainEventsAsync(user.GetEvents());

            return CommandResult<int>.Ok(user.Id) ;
        }
    }
}
