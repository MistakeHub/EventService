using EventService.ObjectStorage.HttpService;
using MediatR;
using SC.Internship.Common.ScResult;

namespace EventService.Features.Ticket.IssueATicket.notifications.CanclellationPayment;

/// <summary>
/// Отмена платежной операции
/// </summary>
// ReSharper disable once UnusedMember.Global
public class CancellationPaymentEventHandler : INotificationHandler<CancellationPaymentEvent>
{
    private readonly HttpServiceClient _httpServiceClient;

    /// <summary>
    /// Конструктор
    /// </summary>
    public CancellationPaymentEventHandler(HttpServiceClient httpServiceClient)
    {
        _httpServiceClient = httpServiceClient;
    }

    /// <inheritdoc />
    public async Task Handle(CancellationPaymentEvent notification, CancellationToken cancellationToken)
    {
        await _httpServiceClient.SendRequest<ScResult<Guid>>("payment", $"{notification.IdPayment}/cancellation", "Put");
    }
}