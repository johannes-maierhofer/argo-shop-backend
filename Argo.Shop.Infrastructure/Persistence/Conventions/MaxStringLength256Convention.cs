using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace Argo.Shop.Infrastructure.Persistence.Conventions
{
    public class MaxStringLength256Convention : IModelFinalizingConvention
    {
        public void ProcessModelFinalizing(IConventionModelBuilder modelBuilder,
            IConventionContext<IConventionModelBuilder> context)
        {
            foreach (var property in modelBuilder.Metadata.GetEntityTypes()
                         .SelectMany(entityType =>
                             entityType.GetDeclaredProperties().Where(
                                 property => property.ClrType == typeof(string))))
            {
                property.Builder.HasMaxLength(256);
            }
        }
    }


}
