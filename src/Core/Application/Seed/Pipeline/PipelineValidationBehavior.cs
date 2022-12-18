using FluentValidation;
using FluentValidation.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OSRS.Application.Seed.Pipeline
{
    public class PipelineValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> 
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        public PipelineValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var validationFailures = _validators
            .Select(validator => validator.Validate(request))
            .SelectMany(validationResult => validationResult.Errors)
            .Where(validationFailure => validationFailure != null)
            .ToList();

            if (validationFailures.Any())
                throw new PipelineValidationException(validationFailures);

            try
            {
                return await next();
            }
            catch(Exception exc) 
            {
                Console.Write("");
                throw;
            }
        }
    }

    public class PipelineValidationException : Exception
    {
        public PipelineValidationException(List<ValidationFailure> validationErrors)
        {
            ValidationErrors = validationErrors;
        }

        public List<ValidationFailure> ValidationErrors { get; }
    }
}
