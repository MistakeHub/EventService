using MediatR;
using SC.Internship.Common.ScResult;

namespace EventService.Features.Ticket.HaveATicket;

/// <summary>
/// Команда проверки наличия билета на мероприятие
/// </summary>
public class HaveATicketCommand : IRequest<ScResult<bool>>
{

    /// <summary>
    /// Id пользователя
    /// </summary>
    public Guid IdOwner { get; set; }

    /// <summary>
    /// Id мероприятия
    /// </summary>
    public Guid IdEvent { get; set; }
}