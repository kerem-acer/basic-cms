using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces.Persistence.Main;
using Domain.Entities;
using FluentValidation;

namespace Application.Modules.Pages.Commands.Create
{
    public class CreatePageCommandValidator : AbstractValidator<CreatePageCommand>
    {
        private readonly IMainAsyncRepository<Page> _repository;

        public CreatePageCommandValidator(IMainAsyncRepository<Page> repository)
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

        private async Task<bool> BeUniqueLink(string link, CancellationToken cancellationToken) =>
            !await _repository.AnyAsync(p => p.Link == link);
    }
}