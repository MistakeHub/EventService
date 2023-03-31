using AutoMapper;

using EventService.Infrastructure.Interfaces;
using MediatR;
using SC.Internship.Common.ScResult;

namespace EventService.Features.Event.GetAll;

// ReSharper disable once UnusedMember.Global Решарпер предлагает удалить хэндлер команды, т.к он явно нигде не используется
/// <summary>
/// Класс обработчик команды получения всех мероприятий
/// </summary>
public class GetAllEventsCommandQueryHandler : IRequestHandler<GetAllEventsCommand, ScResult<List<EventViewModel>>>
{
    private readonly IBaseEventService _baseEventService;
    private readonly IMapper _mapper;
    /// <summary>
    /// Конструктор
    /// </summary>
  
    public GetAllEventsCommandQueryHandler(IBaseEventService baseEventService, IMapper mapper) { _baseEventService= baseEventService; _mapper = mapper; }
    /// <summary>
    /// Обработчик
    /// </summary>

    public async Task<ScResult<List<EventViewModel>>> Handle(GetAllEventsCommand request, CancellationToken cancellationToken)
    {
        var returnGet = await _baseEventService.GetAllEvents();
        var events =_mapper.Map<List<EventViewModel>>(returnGet);

        return new ScResult<List<EventViewModel>> { Result = events };
    }
}