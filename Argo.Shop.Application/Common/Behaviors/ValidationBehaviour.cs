using Argo.Shop.Application.Features;
using FluentValidation;
using MediatR;

namespace Argo.Shop.Application.Common.Behaviors
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
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);

                var validationResults = await Task.WhenAll(
                    _validators.Select(v =>
                        v.ValidateAsync(context, cancellationToken)));

                var failures = validationResults
                    .Where(r => r.Errors.Any())
                    .SelectMany(r => r.Errors)
                    .ToArray();

                if (failures.Any())
                {
                    // handle special case Result, Result<T>
                    if (typeof(TResponse).IsOfTypeResult())
                    {
                        var failureMessages = failures.Select(f => f.ErrorMessage).ToArray();
                        return typeof(TResponse).IsGenericResultType()
                            ? CreateGenericErrorResult(failureMessages)
                            : (TResponse)(object)Result.Error(failureMessages);
                    }

                    throw new ValidationException(failures);
                }
            }

            return await next();
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
