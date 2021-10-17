using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces.Persistence.Main;
using Application.Modules.Containers.Models;
using Domain.Entities;
using Mapster;
using MediatR;

namespace Application.Modules.Containers.Queries
{
    public class GetAllContainersQuery : IRequest<IList<ContainerDto>>
    {
    }

    public class GetAllContainersQueryHandler : IRequestHandler<GetAllContainersQuery, IList<ContainerDto>>
    {
        private readonly IMainAsyncRepository<Container> _repository;

        public GetAllContainersQueryHandler(IMainAsyncRepository<Container> repository)
        {
            _repository = repository;
        }

        public async Task<IList<ContainerDto>> Handle(GetAllContainersQuery request, CancellationToken cancellationToken)
        {
            var containers = await _repository.GetAllAsync(cancellationToken);
            var mappedContainers = containers.Adapt<IList<ContainerDto>>();

            return mappedContainers;
        }
    }
}