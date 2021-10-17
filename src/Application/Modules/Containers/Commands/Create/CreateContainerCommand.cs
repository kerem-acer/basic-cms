using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces.Persistence.Main;
using Domain.Entities;
using Mapster;
using MediatR;

namespace Application.Modules.Containers.Commands.Create
{
    public class CreateContainerCommand : IRequest<int>
    {
        public string Name { get; set; }

        public string Content { get; set; }

        public int PageId { get; set; }
    }

    public class CreateContainerCommandHandler : IRequestHandler<CreateContainerCommand, int>
    {
        private readonly IMainAsyncRepository<Container> _repository;

        public CreateContainerCommandHandler(IMainAsyncRepository<Container> repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(CreateContainerCommand request, CancellationToken cancellationToken)
        {
            Container container = new() 
            {
                Name = request.Name,
                Content = request.Content,
                PageId = request.PageId
            };

            await _repository.AddAsync(container, cancellationToken);
            await _repository.SaveAsync(cancellationToken);

            return container.Id;
        }
    }
}