using EventService.Infrastructure.Interfaces;
using MediatR;
using SC.Internship.Common.Exceptions;
using SC.Internship.Common.ScResult;

namespace EventService.Features.Ticket.HaveATicket;

// ReSharper disable once UnusedMember.Global Решарпер предлагает удалить хэндлер команды, т.к он явно нигде не используется
/// <summary>
/// Класс обработчик команды проверки наличия билета
/// </summary>
public class HaveATicketCommandQueryHandler : IRequestHandler<HaveATicketCommand, ScResult<bool>>
{
    private readonly IBaseEventService _baseEventService;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="baseEventService"></param>
    public HaveATicketCommandQueryHandler(IBaseEventService baseEventService) { _baseEventService = baseEventService; }
    /// <summary>
    /// Обработчик
    /// </summary>

    public async Task<ScResult<bool>> Handle(HaveATicketCommand request, CancellationToken cancellationToken)
    {
        var returnResult = new ScResult<bool>();
        var eventDefault = await _baseEventService.GetEventById(request.IdEvent);

        if (eventDefault == null) throw new ScException("Данное мероприятие не существует");

        var haveATicket = await _baseEventService.HaveATicket(eventDefault, request.IdOwner);

        if (!haveATicket) throw new ScException("У вас нет билета на данное мероприятие");
        returnResult.Result = haveATicket;

        return returnResult;
    }
}