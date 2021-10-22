using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Constants;
using Application.Interfaces.Cache;
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
        private readonly ICacheManager _cacheManager;

        public GetAllPagesQueryHandler(IMainAsyncRepository<Page> repository, 
        ICacheManager cacheManager)
        {
            _repository = repository;
            _cacheManager = cacheManager;
        }

        public async Task<IList<PageDto>> Handle(GetAllPagesQuery request, CancellationToken cancellationToken)
        {
            return await _cacheManager.GetAsync(CacheKeys.GetAllPages, async () => 
            {
                var pages = await _repository.GetAllAsync(cancellationToken, p => p.Containers);
                var mappedPages = pages.Adapt<IList<PageDto>>();

                return mappedPages;
            });
        }
    }
}