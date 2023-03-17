using FluentValidation;

namespace EventService.Features.Event.Commands.Remove
{
    // ReSharper disable once UnusedMember.Global Решарпер рекомендует удалить, так как он не используется
    public class RemoveEventCommandValidation:AbstractValidator<RemoveEventCommand>
    {
        public RemoveEventCommandValidation()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty().WithMessage("Id не может быть пустым");

        }
    }
}
