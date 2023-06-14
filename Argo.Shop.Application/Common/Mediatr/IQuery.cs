using MediatR;

namespace ReviewCleanArch.Application.Common.Mediatr
{
    public interface IQuery<out T> : IRequest<T>
    {
    }
}
