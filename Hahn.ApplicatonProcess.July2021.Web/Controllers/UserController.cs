using Hahn.ApplicationProcess.July2021.Data.Users.Commands.DeleteUser;
using Hahn.ApplicationProcess.July2021.Data.Users.Queries.GetUser;
using Hahn.ApplicationProcess.July2021.Web.Common;
using Hahn.ApplicationProcess.July2021.Web.Integration;
using Hahn.ApplicationProcess.July2021.Web.Integration.Users.Requests;
using Hahn.ApplicationProcess.July2021.Web.Integration.Users.Responses;
using Hahn.ApplicationProcess.July2021.Web.Swagger.Examples;
using Hahn.ApplicationProcess.July2021.Web.Tools;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.July2021.Web.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    [Produces("application/json")]
    [ProducesResponseType(400, Type = typeof(ErrorResponse))]
    [ProducesResponseType(500, Type = typeof(ErrorResponse))]
    public class UserController : BaseController
    {
        private readonly ILogger<UserController> _logger;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public UserController(ILogger<UserController> logger, IMediator mediator, IMapper mapper)
        {
            _logger = logger;
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost("create")]
        [ProducesResponseType(201, Type = typeof(CreateUserResponse))]
        [SwaggerRequestExample(typeof(UserRequest), typeof(UserRequestModelExample))]
        public async Task<IActionResult> Create(UserRequest request)
        {
            var command = _mapper.ToAddUserCommand(request);
            var res = await _mediator.Send(command);

            return SendResultWithData(res, (data) =>
            {
                return Created(new Uri($"https://localhost:44393/get/{res.Data}"),
                    new CreateUserResponse()
                    {
                        id = data.Data
                    });
            });

        }

        [HttpGet("get/{user_id}")]
        [ProducesResponseType(200, Type = typeof(UserResponse))]
        public async Task<IActionResult> Get(int user_id)
        {
            var query = new GetUserQuery()
            {
                UserId = user_id
            };
            var handlerRes = await _mediator.Send(query);

            return SendResultWithData(handlerRes, (data) =>
            {
                var res = _mapper.ToUserResponse(data.Data);
                return Ok(res);
            });
        }

        [HttpPut("update/{user_id}")]
        [ProducesResponseType(200)]
        [SwaggerRequestExample(typeof(UserRequest), typeof(UserRequestModelExample))]
        public async Task<IActionResult> Update(int user_id, UserRequest request)
        {
            var command = _mapper.ToEditUserCommand(request);
            command.Id = user_id;

            var res = await _mediator.Send(command);

            return SendResult(res);
        }

        [HttpDelete("delete/{user_id}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Delete(int user_id)
        {
            var command = new DeleteUserCommand()
            {
                UserId = user_id
            };

            var res = await _mediator.Send(command);

            return SendResult(res);
        }

    }
}
