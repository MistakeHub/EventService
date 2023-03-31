using MediatR;

namespace EventService.Features.Ticket.IssueATicket.notifications.ConfirmationPayment;

/// <summary>
/// Событие подтверждения платежной операции
/// </summary>
public class ConfirmationPaymentEvent : INotification
{
    /// <summary>
    /// Id платежной операции
    /// </summary>
    public Guid IdPayment { get; set; }
}