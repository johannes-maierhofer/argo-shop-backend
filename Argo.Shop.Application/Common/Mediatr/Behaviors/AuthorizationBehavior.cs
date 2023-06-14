using MediatR;
using System.Reflection;
using Argo.Shop.Application.Common.Exceptions;
using Argo.Shop.Application.Common.Identity;
using Argo.Shop.Application.Common.Models;
using AuthorizeAttribute = Argo.Shop.Application.Common.Security.AuthorizeAttribute;

namespace Argo.Shop.Application.Common.Mediatr.Behaviors
{
    public class AuthorizationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IIdentityService _identityService;

        public AuthorizationBehaviour(
            ICurrentUserService currentUserService,
            IIdentityService identityService)
        {
            _currentUserService = currentUserService;
            _identityService = identityService;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            var authorizeAttributes = request
                .GetType()
                .GetCustomAttributes<AuthorizeAttribute>()
                .ToArray();

            if (!authorizeAttributes.Any())
                return await next(); // authorization not required

            // user must be logged in
            var userId = _currentUserService.UserId;
            if(userId == null)
                return Unauthorized();

            // Role-based authorization
            var authorizedRoles = authorizeAttributes
                .Where(a => !string.IsNullOrWhiteSpace(a.Roles))
                .SelectMany(a => a.Roles.Split(','))
                .Select(r => r.Trim())
                .ToArray();

            if (authorizedRoles.Any())
            {
                // Must be a member of at least one role in roles
                bool authorized = false;
                foreach (var role in authorizedRoles)
                {
                    authorized = await _identityService.IsInRoleAsync(userId, role);
                    if(authorized)
                        break;
                }

                if (!authorized)
                    return Unauthorized();
            }

            // Policy-based authorization
            var authorizationPolicies = authorizeAttributes
                .Where(a => !string.IsNullOrWhiteSpace(a.Policy))
                .Select(a => a.Policy)
                .ToArray();

            foreach (var policy in authorizationPolicies)
            {
                var authorized = await _identityService.AuthorizeAsync(userId, policy);

                // all policies must be satisfied
                if (!authorized)
                    return Unauthorized();
            }

            // User is authorized
            return await next();
        }

        private static TResponse Unauthorized(string message = "Unauthorized")
        {
            if (typeof(TResponse).IsOfTypeResult())
            {
                return typeof(TResponse).IsGenericResultType()
                    ? CreateGenericResult(message)
                    : (TResponse)(object)Result.Unauthorized(message);
            }

            throw new ForbiddenAccessException();
        }

        private static TResponse CreateGenericResult(string message)
        {
            var resultType = typeof(TResponse)
                .GetGenericArguments()
                .First();

            return (TResponse)(object)Result.CreateGenericInstance(
                resultType,
                ResultStatus.Unauthorized,
                message);
        }
    }
}