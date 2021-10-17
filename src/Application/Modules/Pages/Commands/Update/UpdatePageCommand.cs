using System.Threading;
using System.Threading.Tasks;
using Application.Exceptions;
using Application.Interfaces.Persistence.Main;
using Domain.Entities;
using MediatR;

namespace Application.Modules.Pages.Commands.Update 
{
    public class UpdatePageCommand : IRequest 
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Link { get; set; }
    }

    public class UpdatePageCommandHandler : IRequestHandler<UpdatePageCommand>
    {
        private readonly IMainAsyncRepository<Page> _repository;

        public UpdatePageCommandHandler(IMainAsyncRepository<Page> repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(UpdatePageCommand request, CancellationToken cancellationToken)
        {
            var page = await _repository.GetByIdAsync(request.Id);

            if (page is null)
                throw new NotFoundException(nameof(Page), request.Id);

            page.Name = request.Name;
            page.Link = request.Link;

            await _repository.SaveAsync(cancellationToken);

            return Unit.Value;
        }
    }
}