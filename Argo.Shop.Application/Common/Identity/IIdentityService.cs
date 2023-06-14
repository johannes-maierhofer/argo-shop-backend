using System.Security.Claims;
using Argo.Shop.Application.Common.Models;

namespace Argo.Shop.Application.Common.Identity
{
    public interface IIdentityService
    {
        Task<string> GetUserNameAsync(string userId);

        Task<bool> IsInRoleAsync(string userId, string role);

        Task<bool> AuthorizeAsync(string userId, string policyName);

        Task<(Result Result, string UserId)> CreateUserAsync(string userName, string password);

        Task<Result> DeleteUserAsync(string userId);

        Task<UserValidationResult> ValidateUserAsync(string? userName, string? password);
    }

    public class UserValidationResult
    {
        public bool IsValid { get; set; }
        public ClaimsIdentity? ClaimsIdentity { get; set; }
        public string? UserId { get; set; }
    }
}