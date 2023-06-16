using Argo.Shop.Domain.Common;
using Argo.Shop.Domain.Common.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Argo.Shop.Infrastructure.Persistence.Interceptors
{
    public class DomainEventDispatchingInterceptor : SaveChangesInterceptor
    {
        private readonly IDomainEventPublisher _domainEventPublisher;

        public DomainEventDispatchingInterceptor(IDomainEventPublisher domainEventPublisher)
        {
            _domainEventPublisher = domainEventPublisher;
        }

        public override int SavedChanges(SaveChangesCompletedEventData eventData, int result)
        {
            return SavedChangesAsync(eventData, result)
                .GetAwaiter()
                .GetResult();
        }

        public override async ValueTask<int> SavedChangesAsync(
            SaveChangesCompletedEventData eventData, 
            int result,
            CancellationToken cancellationToken = new())
        {
            await DispatchDomainEvents(eventData.Context);
            return result;
        }

        private async Task DispatchDomainEvents(DbContext? context)
        {
            if (context == null) return;

            var entities = context.ChangeTracker
                .Entries<IHasDomainEvents>()
                .Where(e => e.Entity.DomainEvents.Any())
                .Select(e => e.Entity)
                .ToList();

            var domainEvents = entities
                .SelectMany(e => e.DomainEvents)
                .ToList();

            entities
                .ToList()
                .ForEach(e => e.ClearDomainEvents());

            foreach (var domainEvent in domainEvents)
                await _domainEventPublisher.Publish(domainEvent);
        }
    }
}
