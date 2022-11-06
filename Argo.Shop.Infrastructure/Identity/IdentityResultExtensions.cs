using Argo.Shop.Application.Features;
using Microsoft.AspNetCore.Identity;

namespace Argo.Shop.Infrastructure.Identity;

public static class IdentityResultExtensions
{
    public static Result ToApplicationResult(this IdentityResult result)
    {
        return result.Succeeded
            ? Result.Success()
            : Result.Error(result.Errors.Select(e => e.Description));
    }
}
