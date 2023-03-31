
using AutoMapper;
using EventService.Infrastructure.Interfaces;
using MediatR;
using SC.Internship.Common.Exceptions;
using SC.Internship.Common.ScResult;


namespace EventService.Features.Event.Create;

// ReSharper disable once UnusedMember.Global Решарпер предлагает удалить хэндлер команды, т.к он явно нигде не используется
/// <summary>
/// Класс обработчик запроса команды создания мероприятия
/// </summary>
public class CreateEventCommandRequestHandler : IRequestHandler<CreateEventCommand, ScResult<string>>
{
    private readonly IBaseEventService _baseEventService;
    private readonly IMapper _mapper;
    /// <summary>
    /// Конструктор
    /// </summary>

    public CreateEventCommandRequestHandler(IBaseEventService baseEventService, IMapper mapper) { _baseEventService= baseEventService;
        _mapper = mapper;
    }
    /// <summary>
    /// Обработчик
    /// </summary>

    public async Task<ScResult<string>> Handle(CreateEventCommand request, CancellationToken cancellationToken)
    {
          

        var resultOfAdd =await _baseEventService.CreateEvent(_mapper.Map<Event>(request));

        if (!resultOfAdd) throw new ScException("Мероприятие не было создано");

        var returnResult  =new ScResult<string>("Мероприятие было создано")  ;
                
        return returnResult;

           

    }
}