using Argo.Shop.Application.Common.Models;
using FluentValidation;
using MediatR;

namespace Argo.Shop.Application.Common.Mediatr.Behaviors
{
    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (!_validators.Any()) 
                return await next();

            var context = new ValidationContext<TRequest>(request);

            var validationResults = await Task.WhenAll(
                _validators.Select(v =>
                    v.ValidateAsync(context, cancellationToken)));

            var failures = validationResults
                .Where(r => r.Errors.Any())
                .SelectMany(r => r.Errors)
                .ToArray();

            if (!failures.Any()) 
                return await next();

            if (!typeof(TResponse).IsOfTypeResult()) 
                throw new Exceptions.ValidationException(failures);

            // handle special case Result, Result<T>
            var failureMessages = failures.Select(f => f.ErrorMessage).ToArray();
            return typeof(TResponse).IsGenericResultType()
                ? CreateGenericErrorResult(failureMessages)
                : (TResponse)(object)Result.Invalid(failureMessages);
        }

        private static TResponse CreateGenericErrorResult(string[] validationFailures)
        {
            var resultType = typeof(TResponse).GetGenericArguments().First();

            return (TResponse)(object)Result.CreateGenericInstance(
                resultType,
                ResultStatus.Invalid,
                validationFailures);
        }
    }
}
