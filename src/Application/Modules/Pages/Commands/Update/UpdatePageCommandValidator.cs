using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces.Persistence.Main;
using Domain.Entities;
using FluentValidation;

namespace Application.Modules.Pages.Commands.Update
{
    public class UpdatePageCommandValidator : AbstractValidator<UpdatePageCommand>
    {
        private readonly IMainAsyncRepository<Page> _repository;

        public UpdatePageCommandValidator(IMainAsyncRepository<Page> repository)
        {
            _repository = repository;

            RuleFor(p => p.Name)
                .NotEmpty()
                .MaximumLength(200);
            
            RuleFor(p => p.Link)
                .NotEmpty()
                .MaximumLength(100)
                .MustAsync(BeUniqueLink).WithMessage("Link already exists!");
        }

        private async Task<bool> BeUniqueLink(UpdatePageCommand command, string link, CancellationToken cancellationToken) =>
            !await _repository.AnyAsync(p => p.Link == link && p.Id != command.Id);
    }
}