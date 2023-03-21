
using EventService.Models.Interfaces;
using MediatR;
using SC.Internship.Common.Exceptions;
using SC.Internship.Common.ScResult;

namespace EventService.Features.Ticket.Commands.IssueATicket;

// ReSharper disable once UnusedMember.Global Решарпер предлагает удалить хэндлер команды, т.к он явно нигде не используется
/// <summary>
/// Класс обработчик команды выдачи билета на мероприятие
/// </summary>
public class IssueATicketCommandRequestHandler : IRequestHandler<IssueATicketCommand, ScResult<Models.Entities.Ticket>>
{
    private readonly IBaseEventService _baseEventService;

    /// <summary>
    /// Конструктор
    /// </summary>
    public IssueATicketCommandRequestHandler(IBaseEventService baseEventService) { _baseEventService = baseEventService; }
    /// <summary>
    /// Обработчик
    /// </summary>
    public Task<ScResult<Models.Entities.Ticket>> Handle(IssueATicketCommand request, CancellationToken cancellationToken)
    {
        var returnResult = new ScResult<Models.Entities.Ticket>();

        var ticket = _baseEventService.IssueTicket(request.IdEvent, request.IdOwner);

        if (ticket == null) throw new ScException("Невозможно выдать билет");

        returnResult.Result = ticket;

        return Task.FromResult(returnResult);
    }
}