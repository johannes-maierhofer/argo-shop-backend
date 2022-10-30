namespace Argo.Shop.Domain.Common
{
    public abstract class Entity<TId> where TId : struct
    {
        public TId Id { get; set; }
    }
}
