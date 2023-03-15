using FluentValidation;

namespace EventService.EntityActivities.EventActiv.Commands.Remove
{
    public class RemoveEventCommandValidation:AbstractValidator<RemoveEventCommand>
    {
        public RemoveEventCommandValidation()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty().WithMessage("Id не может быть пустым");

        }
    }
}
