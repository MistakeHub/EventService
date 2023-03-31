using FluentValidation;

namespace EventService.Features.Event.Remove;

// ReSharper disable once UnusedMember.Global Решарпер рекомендует удалить, так как он не используется
/// <summary>
/// Класс конфигурации валидации команды удаления мероприятия
/// </summary>
public class RemoveEventCommandValidation:AbstractValidator<RemoveEventCommand>
{
    /// <summary>
    /// конфигурация
    /// </summary>
    public RemoveEventCommandValidation()
    {
        RuleFor(x => x.Id).NotNull().NotEmpty().WithMessage("Id не может быть пустым");

    }
}