using Argo.Shop.Application.Features;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Argo.Shop.Application.Common.Behaviors
{
    public class UnhandledExceptionBehaviourOfTypeResult<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : Result
    {
        private readonly ILogger<TRequest> _logger;

        public UnhandledExceptionBehaviourOfTypeResult(ILogger<TRequest> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            try
            {
                return await next();
            }
            catch (Exception ex)
            {
                var requestName = typeof(TRequest).Name;

                _logger.LogError(
                    ex,
                    "Unhandled Exception for Request {Name} {@Request}",
                    requestName,
                    request);

                return typeof(TResponse).IsGenericResultType()
                    ? CreateGenericErrorResult(ex.Message)
                    : (TResponse)Result.Error(ex.Message);
            }
        }

        private static TResponse CreateGenericErrorResult(string errorMessage)
        {
            var resultType = typeof(TResponse).GetGenericArguments().First();

            return (TResponse)Result.CreateGenericInstance(
                resultType, 
                ResultStatus.Error, 
                errorMessage);
        }
    }
}
