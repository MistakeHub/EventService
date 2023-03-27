

using EventService.Models.Interfaces;

using FluentValidation;
using MediatR;
using SC.Internship.Common.Exceptions;
using SC.Internship.Common.ScResult;

namespace EventService.Features.Event.Commands.Update;

// ReSharper disable once UnusedMember.Global Решарпер предлагает удалить хэндлер команды, т.к он явно нигде не используется
/// <summary>
/// Класс обработчик команды обновления мероприятия
/// </summary>
public class UpdateEventCommandRequestHandler : IRequestHandler<UpdateEventCommand, ScResult<string>>
{
    private readonly IBaseEventService _baseEventService;

    /// <summary>
    /// Конструктор
    /// </summary>
    public UpdateEventCommandRequestHandler(IBaseEventService baseEventService)
    {
       
        _baseEventService = baseEventService; }
    /// <summary>
    /// Обработчик 
    /// </summary>

    public Task<ScResult<string>> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
    {
        var returnResult = new ScResult<string>();

        var validator = new UpdateEventCommandValidation();
        var validationResult = validator.Validate(request);
        if (!validationResult.IsValid) throw new ScException(new ValidationException(validationResult.Errors), "ValidationException");

      
        var resultUpdate = _baseEventService.UpdateEvent(request.Id, request.Start, request.End, request.Title, request.Description, request.IdImage, request.IdSpace);
        if (!resultUpdate) throw new ScException("Мероприятие не было обновлено");
        returnResult.Result=  "Мероприятие было обновлено";
       

        return Task.FromResult(returnResult);
    }
}