using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces.Persistence.Main;
using Application.Modules.Pages.Models;
using Domain.Entities;
using Mapster;
using MediatR;

namespace Application.Modules.Pages.Queries 
{
    public class GetAllPagesQuery : IRequest<IList<PageDto>>
    {
    }

    public class GetAllPagesQueryHandler : IRequestHandler<GetAllPagesQuery, IList<PageDto>>
    {
        private readonly IMainAsyncRepository<Page> _repository;

        public GetAllPagesQueryHandler(IMainAsyncRepository<Page> repository)
        {
            _repository = repository;
        }

        public async Task<IList<PageDto>> Handle(GetAllPagesQuery request, CancellationToken cancellationToken)
        {
            var pages = await _repository.GetAllAsync(cancellationToken, p => p.Containers);
            var mappedPages = pages.Adapt<IList<PageDto>>();

            return mappedPages;
        }
    }
}