using EventService.Models.Interfaces;
using MediatR;
using SC.Internship.Common.Exceptions;
using SC.Internship.Common.ScResult;

namespace EventService.Features.Ticket.Commands.CheckSeat;

// ReSharper disable once UnusedMember.Global Решарпер предлагает удалить хэндлер команды, т.к он явно нигде не используется
/// <summary>
/// Класс обработчик команды проверки наличия места
/// </summary>
public class CheckSeatCommandQueryHandler:IRequestHandler<CheckSeatCommand,ScResult<int?>>
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

    public Task<ScResult<int?>> Handle(CheckSeatCommand request, CancellationToken cancellationToken)
    {
        var returnResult = new ScResult<int?>();

        var seat = _baseEventService.CheckSeat(request.IdEvent, request.IdTicket);

        if (seat==null) throw new ScException("На данный билет место незарезервированно");

        returnResult.Result = seat;

        return Task.FromResult(returnResult);
    }
}