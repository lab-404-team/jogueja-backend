using Core.Shared.Results;
using MediatR;

namespace Core.Application.Messaging
{
    public interface IQuery<TResponse> : IRequest<Result<TResponse>> { }
}
