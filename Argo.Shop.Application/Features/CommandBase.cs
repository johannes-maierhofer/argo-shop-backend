using MediatR;

namespace Argo.Shop.Application.Features
{
    public abstract class CommandBase<T> : IRequest<T>
    {
    }
}
