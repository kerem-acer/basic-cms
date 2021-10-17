using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Modules.Containers.Models;
using Application.Interfaces.Persistence.Main;
using Domain.Entities;
using Mapster;
using MediatR;

namespace Application.Modules.Containers.Queries
{
    public class GetContainersByPageIdQuery : IRequest<IList<ContainerDto>>
    {
        public GetContainersByPageIdQuery(int pageId)
        {
            PageId = pageId;
        }

        public int PageId { get; set; }
    }

    public class GetContainersByPageIdQueryHandler : IRequestHandler<GetContainersByPageIdQuery, IList<ContainerDto>>
    {
        private readonly IMainAsyncRepository<Container> _repository;

        public GetContainersByPageIdQueryHandler(IMainAsyncRepository<Container> repository)
        {
            _repository = repository;
        }

        public async Task<IList<ContainerDto>> Handle(GetContainersByPageIdQuery request, CancellationToken cancellationToken)
        {
            var containers = await _repository.GetListAsync(container => container.PageId == request.PageId, cancellationToken);
            var mappedContainers = containers.Adapt<IList<ContainerDto>>();

            return mappedContainers;
        }
    }
}