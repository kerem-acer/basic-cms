using System.Threading;
using System.Threading.Tasks;
using Application.Exceptions;
using Application.Interfaces.Persistence.Main;
using Domain.Entities;
using MediatR;

namespace Application.Modules.Containers.Commands.Update
{
    public class UpdateContainerCommand : IRequest
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Content { get; set; }
    }

    public class UpdateContainerCommandHandler : IRequestHandler<UpdateContainerCommand>
    {
        private readonly IMainAsyncRepository<Container> _repository;

        public UpdateContainerCommandHandler(IMainAsyncRepository<Container> repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(UpdateContainerCommand request, CancellationToken cancellationToken)
        {
            var container = await _repository.GetByIdAsync(request.Id);

            if (container is null)
                throw new NotFoundException(nameof(Container), request.Id);

            container.Name = request.Name;
            container.Content = request.Content;

            await _repository.SaveAsync(cancellationToken);

            return Unit.Value;
        }
    }
}