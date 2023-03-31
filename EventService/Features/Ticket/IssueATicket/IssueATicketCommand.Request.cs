using MediatR;
using SC.Internship.Common.ScResult;

namespace EventService.Features.Ticket.IssueATicket;

/// <summary>
/// Команда выдачи билета на мероприятие
/// </summary>
public class IssueATicketCommand : IRequest<ScResult<Ticket>>
{

    /// <summary>
    /// Id мероприятия
    /// </summary>
    public Guid IdEvent { get; set; }

    /// <summary>
    /// Id пользователя
    /// </summary>
    public Guid IdOwner { get; set; }

    /// <summary>
    /// Цена
    /// </summary>
    public decimal Price { get; set; }
    /// <summary>
    /// поле для метода авторизации
    /// </summary>
    public string? Authorization { get; set; } = null!;
}