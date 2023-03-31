using MediatR;

namespace EventService.Features.Ticket.IssueATicket.notifications.CanclellationPayment;

/// <summary>
/// Событие отмены платежной операции 
/// </summary>
public class CancellationPaymentEvent : INotification
{
    /// <summary>
    /// Id Платежной операции
    /// </summary>
    public Guid IdPayment { get; set; }
}