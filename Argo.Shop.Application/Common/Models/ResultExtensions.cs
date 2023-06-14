using Argo.Shop.Domain.Common.Extensions;

namespace Argo.Shop.Application.Common.Models
{
    public static class ResultExtensions
    {
        public static bool IsOfTypeResult(this Type type)
        {
            return type.IsNonGenericResultType() || type.IsGenericResultType();
        }

        public static bool IsNonGenericResultType(this Type type)
        {
            return type.IsAssignableTo(typeof(Result));
        }

        public static bool IsGenericResultType(this Type type)
        {
            return type.IsAssignableToGenericType(typeof(Result<>));
        }
    }
}
