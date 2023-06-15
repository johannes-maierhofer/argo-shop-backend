using Argo.Shop.Application.Common.Identity;

namespace Argo.Shop.IntegrationTests.Testing.Services
{
    public class FakeCurrentUserService : ICurrentUserService
    {
        private readonly TestScopeAccessor _testScopeAccessor;

        public FakeCurrentUserService(TestScopeAccessor testScopeAccessor)
        {
            _testScopeAccessor = testScopeAccessor;
        }

        public string? UserId => _testScopeAccessor.TestScope?.CurrentUserId;
    }
}
