
using FluentValidation;

namespace EventService.Features.Event.Create;

/// <summary>
/// Класс конфигурации валидации команды создания мероприятия
/// </summary>
// ReSharper disable once UnusedMember.Global
public class CreateEventCommandValidation : AbstractValidator<CreateEventCommand>
{
    /// <summary>
    /// Конфигурация валидации команды создания мероприятия
    /// </summary>
    public CreateEventCommandValidation()
    {

        RuleFor(x => x.Start).NotNull().LessThan(v => v.End).WithMessage("Некорректная дата начала");
        RuleFor(x => x.Title).NotNull().NotEmpty().WithMessage("Название не может быть пустым").MaximumLength(15).WithMessage("Максимальная длина названия 15 символов");
        RuleFor(x => x.IdImage).NotNull().NotEmpty().WithMessage("Id изображения не может быть пустым");
        RuleFor(x => x.IdSpace).NotNull().NotEmpty().WithMessage("Id пространства не может быть пустым");
        RuleFor(x => x.Description).NotNull().NotEmpty().WithMessage("Описание не может быть пустым").MaximumLength(100).WithMessage("Максимальная длина описания 100 символов");

    }
}