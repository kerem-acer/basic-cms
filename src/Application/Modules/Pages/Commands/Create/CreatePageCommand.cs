using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces.Persistence.Main;
using Domain.Entities;
using MediatR;

namespace Application.Modules.Pages.Commands.Create
{
    public class CreatePageCommand : IRequest<int>
    {
        public string Name { get; set; }

        public string Link { get; set; }
    }

    public class CreatePageCommandHandler : IRequestHandler<CreatePageCommand, int>
    {
        private readonly IMainAsyncRepository<Page> _repository;

        public CreatePageCommandHandler(IMainAsyncRepository<Page> repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(CreatePageCommand request, CancellationToken cancellationToken)
        {
            Page page = new() 
            {
                Name = request.Name,
                Link = request.Link
            };

            await _repository.AddAsync(page, cancellationToken);
            await _repository.SaveAsync(cancellationToken);

            return page.Id;
        }
    }
}