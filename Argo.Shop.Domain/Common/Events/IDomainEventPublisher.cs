namespace Argo.Shop.Domain.Common.Events
{
    public interface IDomainEventPublisher
    {
        Task Publish(DomainEvent domainEvent);
    }
}
