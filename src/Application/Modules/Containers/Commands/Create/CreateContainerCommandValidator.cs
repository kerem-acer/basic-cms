using FluentValidation;

namespace Application.Modules.Containers.Commands.Create
{
    public class CreateContainerCommandValidator : AbstractValidator<CreateContainerCommand>
    {
        public CreateContainerCommandValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(p => p.Content)
                .NotEmpty();
            
            RuleFor(p => p.PageId)
                .NotEmpty();
        }
    }
}