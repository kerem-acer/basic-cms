using System.Threading;
using System.Threading.Tasks;
using Application.Exceptions;
using Application.Interfaces.Persistence.Main;
using Domain.Entities;
using MediatR;

namespace Application.Modules.Pages.Commands.Remove
{
    public class RemovePageCommand : IRequest
    {
        public RemovePageCommand(int pageId)
        {
            PageId = pageId;
        }

        public int PageId { get; set; }
    }

    public class RemovePageCommandHandler : IRequestHandler<RemovePageCommand>
    {
        private readonly IMainAsyncRepository<Page> _repository;

        public RemovePageCommandHandler(IMainAsyncRepository<Page> repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(RemovePageCommand request, CancellationToken cancellationToken)
        {
            var page = await _repository.GetByIdAsync(request.PageId);

            if (page is null)
                throw new NotFoundException(nameof(Page), request.PageId);

            await _repository.RemoveAsync(page);
            await _repository.SaveAsync(cancellationToken);

            return Unit.Value;
        }
    }
}