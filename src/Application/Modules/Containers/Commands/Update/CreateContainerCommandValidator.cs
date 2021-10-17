using FluentValidation;

namespace Application.Modules.Containers.Commands.Update
{
    public class UpdateContainerCommandValidator : AbstractValidator<UpdateContainerCommand>
    {
        public UpdateContainerCommandValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(p => p.Content)
                .NotEmpty();
        }
    }
}