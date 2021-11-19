using Hahn.ApplicationProcess.July2021.Data.Common;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Hahn.ApplicationProcess.July2021.Web.Common
{
    public class BaseController : ControllerBase
    {
        protected IActionResult SendResult(CommandResult result)
        {
            if (result.ResultType == RequestResultTypes.Error)
            {
                return BadRequest(result.Exception?.Message);
            }

            return Ok();
        }

        protected IActionResult SendResultWithData<TResult>(CommandResult<TResult> result, Func<CommandResult<TResult>, IActionResult> actOnData)
        {
            if (result.ResultType == RequestResultTypes.Error)
            {
                throw result.Exception;
                //return BadRequest();
            }
            else if (result.ResultType == RequestResultTypes.UnhandledError)
            {
                throw result.Exception;
                //return StatusCode(500, result.Exception?.Message);
            }

            return actOnData(result);
        }


    }
}
