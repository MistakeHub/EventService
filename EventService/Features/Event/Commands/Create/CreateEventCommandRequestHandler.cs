
using EventService.Models.Interfaces;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using SC.Internship.Common.Exceptions;
using SC.Internship.Common.ScResult;

namespace EventService.Features.Event.Commands.Create;

// ReSharper disable once UnusedMember.Global Решарпер предлагает удалить хэндлер команды, т.к он явно нигде не используется
/// <summary>
/// Класс обработчик запроса команды создания мероприятия
/// </summary>
public class CreateEventCommandRequestHandler : IRequestHandler<CreateEventCommand, ScResult<string>>
{
    private readonly IBaseEventService _baseEventService;

    /// <summary>
    /// Конструктор
    /// </summary>
   
    public CreateEventCommandRequestHandler(IBaseEventService baseEventService) { _baseEventService= baseEventService; }
    /// <summary>
    /// Обработчик
    /// </summary>

    public Task<ScResult<string>> Handle(CreateEventCommand request, CancellationToken cancellationToken)
    {
          
        var validator = new CreateEventCommandValidation();
        // ReSharper disable once RedundantAssignment
        var validationResult =new ValidationResult();
        
        validationResult = validator.Validate(request);
        if (!validationResult.IsValid) throw new ScException(new ValidationException(validationResult.Errors), "ValidationException");
                
        var resultOfAdd = _baseEventService.CreateEvent(request.Start, request.End, request.Title,

            request.Description, request.IdImage, request.IdSpace);

        if (!resultOfAdd) throw new ScException("Мероприятие не было создано");

        var returnResult  =new ScResult<string>("Мероприятие было создано")  ;
                
        return Task.FromResult(returnResult);

           

    }
}