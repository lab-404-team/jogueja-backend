using Core.Shared.Results;
using MediatR;

namespace Core.Application.Messaging
{
    public interface ICommand : IRequest<Result> { }

    public interface ICommand<TResponse> : IRequest<Result<TResponse>> { }
}
