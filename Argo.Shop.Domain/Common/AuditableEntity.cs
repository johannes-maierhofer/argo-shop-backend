namespace Argo.Shop.Domain.Common
{
    public abstract class AuditableEntity<T> : Entity<T>, IAuditable
        where T : struct
    {
        public DateTimeOffset Created { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public DateTimeOffset LastModified { get; set; }
        public string LastModifiedBy { get; set; } = string.Empty;
    }
}
