namespace Argo.Shop.Domain.Common
{
    public interface IAuditable
    {
        public DateTimeOffset Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset LastModified { get; set; }
        public string LastModifiedBy { get; set; }
    }
}
