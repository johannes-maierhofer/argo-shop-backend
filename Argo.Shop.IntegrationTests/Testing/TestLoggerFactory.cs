using Divergic.Logging.Xunit;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace Argo.Shop.IntegrationTests.Testing
{
    /// <summary>
    /// Creates a logger that writes to the XUnit output from the IntegrationTestScope.
    /// </summary>
    public class TestScopeLoggerFactory : ILoggerFactory
    {
        private readonly TestScopeAccessor _testScopeAccessor;
        private readonly LoggingConfig _config;

        public TestScopeLoggerFactory(TestScopeAccessor testScopeAccessor, LoggingConfig config)
        {
            _testScopeAccessor = testScopeAccessor;
            _config = config;
        }

        public ILogger CreateLogger(string categoryName)
        {
            // uses the logger from Divergic.Logging.Xunit
            return new TestOutputLogger(
                categoryName,
                _testScopeAccessor.TestScope?.Output ?? new ConsoleTestOutputHelper(), // ConsoleTestOutputHelper is a mere fallback
                _config);
        }

        public void AddProvider(ILoggerProvider provider)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            // nothing to dispose
        }
    }

    public class ConsoleTestOutputHelper : ITestOutputHelper
    {
        public void WriteLine(string message)
        {
            Console.WriteLine(message);
        }

        public void WriteLine(string format, params object[] args)
        {
            string formattedString = string.Format(format, args);
            Console.WriteLine(formattedString);
        }
    }
}
