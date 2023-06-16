using Argo.Shop.Application.Common.Services;

namespace Argo.Shop.Infrastructure.Services
{
    public sealed class DateTimeService : IDateTimeService
    {
        public DateTimeOffset Now() => DateTimeOffset.Now;
    }
}
