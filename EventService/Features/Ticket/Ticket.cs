namespace EventService.Features.Ticket;

/// <summary>
/// Билет
/// </summary>
public class Ticket
{
    /// <summary>
    /// Id билета
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Id владельца
    /// </summary>
    // ReSharper disable once UnusedMember.Global
    public Guid? IdOwner { get; set; }

    /// <summary>
    /// Место
    /// </summary>
    // ReSharper disable once UnusedMember.Global
    public int? Seat { get; set; }

}