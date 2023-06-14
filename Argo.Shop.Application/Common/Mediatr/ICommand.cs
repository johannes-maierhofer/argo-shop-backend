using MediatR;

namespace ReviewCleanArch.Application.Common.Mediatr
{
    public interface ICommand<out T> : IRequest<T>
    {
    }
}
