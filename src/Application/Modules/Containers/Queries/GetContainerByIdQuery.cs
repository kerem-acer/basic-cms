using System.Threading;
using System.Threading.Tasks;
using Application.Exceptions;
using Application.Interfaces.Persistence.Main;
using Application.Modules.Containers.Models;
using Domain.Entities;
using Mapster;
using MediatR;

namespace Application.Modules.Containers.Queries
{
    public class GetContainerByIdQuery : IRequest<ContainerDto>
    {
        public GetContainerByIdQuery(int containerId)
        {
            ContainerId = containerId;
        }

        public int ContainerId { get; set; }
    }

    public class GetContainerByIdQueryHandler : IRequestHandler<GetContainerByIdQuery, ContainerDto>
    {
        private readonly IMainAsyncRepository<Container> _repository;

        public GetContainerByIdQueryHandler(IMainAsyncRepository<Container> repository)
        {
            _repository = repository;
        }

        public async Task<ContainerDto> Handle(GetContainerByIdQuery request, CancellationToken cancellationToken)
        {
            var container = await _repository.GetByIdAsync(request.ContainerId);

            if (container is null)
                throw new NotFoundException(nameof(Container), request.ContainerId);

            return container.Adapt<ContainerDto>();
        }
    }
}