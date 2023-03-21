using AutoMapper;

using EventService.Models.Interfaces;
using EventService.ObjectStorage.ViewModels;
using MediatR;
using SC.Internship.Common.ScResult;

namespace EventService.Features.Event.Commands.GetAllForTheWeek;

// ReSharper disable once UnusedMember.Global Решарпер предлагает удалить хэндлер команды, т.к он явно нигде не используется
/// <summary>
/// Класс обработчик команды получения всех мероприятий на неделю
/// </summary>
public class GetAllEventsForTheWeekCommandQueryHandler : IRequestHandler<GetAllEventsForTheWeekCommand, ScResult<List<EventViewModel>>>
{
    private readonly IBaseEventService _baseEventService;
    private readonly IMapper _mapper;
    /// <summary>
    /// Конструктор
    /// </summary>

    public GetAllEventsForTheWeekCommandQueryHandler(IBaseEventService baseEventService, IMapper mapper) { _baseEventService=baseEventService; _mapper = mapper; }
    /// <summary>
    /// Обработчик
    /// </summary>
    public Task<ScResult<List<EventViewModel>>> Handle(GetAllEventsForTheWeekCommand request, CancellationToken cancellationToken)
    {
        var returnGet = _baseEventService.GetAllEventsForTheWeek();
        var events = _mapper.Map<List<EventViewModel>>(returnGet);

        return Task.FromResult(new ScResult<List<EventViewModel>>{Result = events});
    }
}