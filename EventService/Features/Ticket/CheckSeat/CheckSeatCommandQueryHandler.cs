using EventService.Infrastructure.Interfaces;
using MediatR;
using SC.Internship.Common.Exceptions;
using SC.Internship.Common.ScResult;

namespace EventService.Features.Ticket.CheckSeat;

// ReSharper disable once UnusedMember.Global Решарпер предлагает удалить хэндлер команды, т.к он явно нигде не используется
/// <summary>
/// Класс обработчик команды проверки наличия места
/// </summary>
public class CheckSeatCommandQueryHandler : IRequestHandler<CheckSeatCommand, ScResult<int?>>
{
    private readonly IBaseEventService _baseEventService;

    /// <summary>
    /// Конструктор
    /// </summary>
    public CheckSeatCommandQueryHandler(IBaseEventService baseEventService)
    {
        _baseEventService = baseEventService;
    }

    /// <summary>
    /// Обработчик
    /// </summary>

    public async Task<ScResult<int?>> Handle(CheckSeatCommand request, CancellationToken cancellationToken)
    {
        var returnResult = new ScResult<int?>();
        var eventDefault = await _baseEventService.GetEventById(request.IdEvent);

        if (eventDefault == null) throw new ScException("Данное мероприятие не существует");
        var seat = await _baseEventService.CheckSeat(eventDefault, request.IdTicket);

        if (seat == null) throw new ScException("На данный билет место незарезервированно");

        returnResult.Result = seat;

        return returnResult;
    }
}