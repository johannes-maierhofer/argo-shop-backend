using Xunit.Abstractions;

namespace Argo.Shop.IntegrationTests
{
    [Collection("Application")]
    public class TestBase
    {
        protected ITestOutputHelper Output { get; }

        protected TestBase(ITestOutputHelper output)
        {
            Output = output;
        }
    }
}
