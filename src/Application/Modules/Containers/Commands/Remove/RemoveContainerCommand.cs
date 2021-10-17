using System.Threading;
using System.Threading.Tasks;
using Application.Exceptions;
using Application.Interfaces.Persistence.Main;
using Domain.Entities;
using MediatR;

namespace Application.Modules.Containers.Commands.Remove
{
    public class RemoveContainerCommand : IRequest
    {
        public RemoveContainerCommand(int containerId)
        {
            ContainerId = containerId;
        }

        public int ContainerId { get; set; }
    }

    public class RemoveContainerCommandHandler : IRequestHandler<RemoveContainerCommand>
    {
        private readonly IMainAsyncRepository<Container> _repository;

        public RemoveContainerCommandHandler(IMainAsyncRepository<Container> repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(RemoveContainerCommand request, CancellationToken cancellationToken)
        {
            var container = await _repository.GetByIdAsync(request.ContainerId);

            if (container is null)
                throw new NotFoundException(nameof(Container), request.ContainerId);

            await _repository.RemoveAsync(container);
            await _repository.SaveAsync(cancellationToken);

            return Unit.Value;
        }
    }
}