﻿using FluentValidation;
using Hahn.ApplicationProcess.July2021.Data.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Hahn.ApplicationProcess.July2021.Domain.Exceptions;
using ValidationException = FluentValidation.ValidationException;

namespace Hahn.ApplicationProcess.July2021.Data.PipelineBehavior
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        //where TResponse : RequestResult<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            // validation
            var context = new FluentValidation.ValidationContext<TRequest>(request);
            var failures = _validators
                .Select(x => x.Validate(context))
                .SelectMany(x => x.Errors)
                .Where(x => x != null)
                .ToList();

            try
            {
                if (failures.Any() == true)
                {
                    throw new ValidationException(failures);
                }

                return await next();

            }
            catch (DomainStateException e)
            {
                var response = Activator.CreateInstance<TResponse>();

                var result = response as CommandResult;
                if (result != null)
                {
                    result.Exception = e;
                    result.ResultType = RequestResultTypes.Error;

                    return response;
                }

                throw;
            }
            catch (Exception e)
            {
                var response = Activator.CreateInstance<TResponse>();
                
                var result = response as CommandResult;
                if (result != null)
                {
                    result.Exception = e;
                    result.ResultType = RequestResultTypes.UnhandledError;

                    return response;
                }

                throw;
            }

        }
    }
}
