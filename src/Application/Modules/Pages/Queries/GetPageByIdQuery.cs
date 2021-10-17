using System.Threading;
using System.Threading.Tasks;
using Application.Exceptions;
using Application.Interfaces.Persistence.Main;
using Application.Modules.Pages.Models;
using Domain.Entities;
using Mapster;
using MediatR;

namespace Application.Modules.Pages.Queries
{
    public class GetPageByIdQuery : IRequest<PageDto>
    {
        public GetPageByIdQuery(int pageId)
        {
            PageId = pageId;
        }

        public int PageId { get; set; }
    }

    public class GetPageByIdQueryHandler : IRequestHandler<GetPageByIdQuery, PageDto>
    {
        private readonly IMainAsyncRepository<Page> _repository;

        public GetPageByIdQueryHandler(IMainAsyncRepository<Page> repository)
        {
            _repository = repository;
        }

        public async Task<PageDto> Handle(GetPageByIdQuery request, CancellationToken cancellationToken)
        {
            var page = await _repository.GetByIdAsync(request.PageId);

            if (page is null)
                throw new NotFoundException(nameof(Page), request.PageId);

            return page.Adapt<PageDto>();
        }
    }
}