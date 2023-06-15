using Argo.Shop.Application.Common.Identity;

namespace Argo.Shop.IntegrationTests.Services
{
    public class FakeCurrentUserService :  ICurrentUserService
    {
        public string? UserId => Testing.CurrentUserId;
    }
}
