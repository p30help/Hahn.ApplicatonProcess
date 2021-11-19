using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Hahn.ApplicationProcess.July2021.Data.Common;
using Hahn.ApplicationProcess.July2021.Domain.Contracts;
using Hahn.ApplicationProcess.July2021.Domain.Contracts.Users;
using Hahn.ApplicationProcess.July2021.Domain.Exceptions;
using Hahn.ApplicationProcess.July2021.Domain.Users;
using MediatR;

namespace Hahn.ApplicationProcess.July2021.Data.Users.Queries.GetUser
{
    public class GetUserHandler : IRequestHandler<GetUserQuery, CommandResult<UserDto>>
    {
        private readonly IUsersReadonlyRepository _readonlyRepository;
        private readonly IHahnUnitOfWork _unitOfWork;

        public GetUserHandler(IUsersReadonlyRepository readonlyRepository, IHahnUnitOfWork unitOfWork)
        {
            _readonlyRepository = readonlyRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<CommandResult<UserDto>> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            // Because using Sql InMemory we can not connect to two different DbContext with same source
            // so I have to get record from IHahnUnitOfWork instead of IUsersReadonlyRepository

            //var item = await _readonlyRepository.GetAsync(request.UserId);

            var itemPrimary = await _unitOfWork.UsersRepository.GetAsync(request.UserId);

            if (itemPrimary == null)
            {
                throw new NotFoundException();
            }

            var item = new UserDto()
            {
                Id = itemPrimary.Id,
                FirstName = itemPrimary.FirstName.Value,
                LastName = itemPrimary.LastName.Value,
                Age = itemPrimary.Age.Value,
                Address = itemPrimary.Address,
                Email = itemPrimary.Email.Value,
                Assets = itemPrimary.Assets?.Select(x => 
                    new AssetDto()
                    {
                        Key = x.Key,
                        Id = x.Id,
                        Name = x.Name,
                        Symbol = x.Symbol
                    }).ToList()
            };


            return CommandResult<UserDto>.Ok(item);
        }
    }
}
