using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using Xunit.Abstractions;

namespace Argo.Shop.IntegrationTests.Testing
{
    public class TestScope : IDisposable
    {
        private readonly IServiceScope? _serviceScope;
        private readonly ITestOutputHelper _output;

        public TestScope(
            IServiceScope serviceScope,
            ITestOutputHelper output,
            string? currentUserId)
        {
            _serviceScope = serviceScope;
            _output = output;
            CurrentUserId = currentUserId;
        }

        public ITestOutputHelper? Output => _output;
        public string? CurrentUserId { get; }

        public T GetRequiredService<T>() where T : notnull
        {
            Debug.Assert(_serviceScope != null, nameof(_serviceScope) + " != null");
            return _serviceScope.ServiceProvider.GetRequiredService<T>();
        }

        /// <summary>
        /// Send request via Mediatr.
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
        {
            var mediator = _serviceScope!.ServiceProvider.GetRequiredService<ISender>();
            return await mediator.Send(request);
        }

        public void Dispose()
        {
            _serviceScope?.Dispose();
        }
    }
}
