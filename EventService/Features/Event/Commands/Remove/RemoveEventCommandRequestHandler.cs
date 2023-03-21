
using EventService.Models.Interfaces;
using FluentValidation;
using MediatR;
using SC.Internship.Common.Exceptions;
using SC.Internship.Common.ScResult;

namespace EventService.Features.Event.Commands.Remove;

// ReSharper disable once UnusedMember.Global Решарпер предлагает удалить хэндлер команды, т.к он явно нигде не используется
/// <summary>
/// Класс обработчик команды удаления мероприятия
/// </summary>
public class RemoveEventCommandRequestHandler : IRequestHandler<RemoveEventCommand, ScResult<string>>
{
    private readonly IBaseEventService _baseEventService;

    /// <summary>
    /// Конструктор
    /// </summary>
    public RemoveEventCommandRequestHandler(IBaseEventService baseEventService) { _baseEventService= baseEventService; }
    /// <summary>
    /// Обработчик 
    /// </summary>
  
    public Task<ScResult<string>> Handle(RemoveEventCommand request, CancellationToken cancellationToken)
    {
        var returnResult = new ScResult<string>();
        var validation = new RemoveEventCommandValidation();
        var errors = validation.Validate(request).Errors;
        if (errors.Count !=0) throw new ScException(new ValidationException(errors),"Мероприятие не было удалено");
        var resultDelete = _baseEventService.DeleteEvent(request.Id);

         
        if (!resultDelete) throw new ScException("Мероприятие не было удалено");
        returnResult.Result= "Мероприятие было удалено";

        return Task.FromResult(returnResult);
    }
}