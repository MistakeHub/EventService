using EventService.Models.Interfaces;
using MediatR;
using SC.Internship.Common.Exceptions;
using SC.Internship.Common.ScResult;

namespace EventService.Features.Ticket.Commands.HaveATicket;

// ReSharper disable once UnusedMember.Global Решарпер предлагает удалить хэндлер команды, т.к он явно нигде не используется
/// <summary>
/// Класс обработчик команды проверки наличия билета
/// </summary>
public class HaveATicketCommandQueryHandler:IRequestHandler<HaveATicketCommand, ScResult<bool>>
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

    public Task<ScResult<bool>> Handle(HaveATicketCommand request, CancellationToken cancellationToken)
    {
        var returnResult = new ScResult<bool>();

        var haveATicket = _baseEventService.HaveATicket(request.IdEvent, request.IdOwner);

        if (!haveATicket) throw new ScException("У вас нет билета на данное мероприятие");
        returnResult.Result=haveATicket;
        
        return Task.FromResult(returnResult);
    }
}