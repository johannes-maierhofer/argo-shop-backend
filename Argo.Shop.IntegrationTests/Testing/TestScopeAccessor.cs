namespace Argo.Shop.IntegrationTests.Testing
{
    // loosely based on Microsoft.AspNetCore.Http.HttpContextAccessor
    public class TestScopeAccessor
    {
        private static readonly AsyncLocal<TestScopeHolder> TestScopeCurrent = new();

        public TestScope? TestScope
        {
            get => TestScopeCurrent.Value?.Context;
            set
            {
                var holder = TestScopeCurrent.Value;
                if (holder != null)
                {
                    // Clear current TestScope trapped in the AsyncLocals, as its done.
                    holder.Context = null;
                }

                if (value != null)
                {
                    // Use an object indirection to hold the TestScope in the AsyncLocal,
                    // so it can be cleared in all ExecutionContexts when its cleared.
                    TestScopeCurrent.Value = new TestScopeHolder { Context = value };
                }
            }
        }

        private sealed class TestScopeHolder
        {
            public TestScope? Context;
        }
    }
}
