using EventService.ObjectStorage.HttpService;
using MediatR;
using SC.Internship.Common.ScResult;

namespace EventService.Features.Ticket.IssueATicket.notifications.ConfirmationPayment;

/// <summary>
/// Подтверждение платежной операции 
/// </summary>
// ReSharper disable once UnusedMember.Global
public class ConfirmationPaymentEventHandler : INotificationHandler<ConfirmationPaymentEvent>
{
    private readonly HttpServiceClient _httpServiceClient;

    /// <summary>
    /// Конструктор
    /// </summary>

    public ConfirmationPaymentEventHandler(HttpServiceClient httpServiceClient)
    {
        _httpServiceClient = httpServiceClient;
    }

    /// <inheritdoc />
    public async Task Handle(ConfirmationPaymentEvent notification, CancellationToken cancellationToken)
    {
        await _httpServiceClient.SendRequest<ScResult<Guid>>("payment", $"{notification.IdPayment}/confirmation", "Put");
    }
}