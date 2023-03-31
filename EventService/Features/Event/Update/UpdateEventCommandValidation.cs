
using FluentValidation;

namespace EventService.Features.Event.Update;

/// <summary>
/// Класс конфигурации валидации команды обновления мероприятия
/// </summary>
// ReSharper disable once UnusedMember.Global
public class UpdateEventCommandValidation:AbstractValidator<UpdateEventCommand>
{
    /// <summary>
    /// Конфигурация
    /// </summary>
    public UpdateEventCommandValidation()
    {
        RuleFor(x => x.Id).NotNull().NotEmpty().WithMessage("Id не может быть пустым");
        RuleFor(x => x.Start).NotNull().LessThan(v => v.End).WithMessage("Некорректная дата начала") ;
        RuleFor(x => x.Title).NotNull().NotEmpty().WithMessage("Название не может быть пустым").MaximumLength(15).WithMessage("Максимальная длина названия 15 символов");
        
        RuleFor(x => x.Description).NotNull().NotEmpty().WithMessage("Описание не может быть пустым").MaximumLength(100).WithMessage("Максимальная длина описания 100 символов");

    }
}