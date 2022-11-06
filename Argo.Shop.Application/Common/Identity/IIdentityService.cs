using Argo.Shop.Application.Features;
using System.Security.Claims;

namespace Argo.Shop.Application.Common.Identity
{
    public interface IIdentityService
    {
        Task<string> GetUserNameAsync(string userId);

        Task<bool> IsInRoleAsync(string userId, string role);

        Task<bool> AuthorizeAsync(string userId, string policyName);

        Task<(Result Result, string UserId)> CreateUserAsync(string userName, string password);

        Task<Result> DeleteUserAsync(string userId);

        Task<Result<ClaimsIdentity>> ValidateUserAsync(string? userName, string? password);
    }
}