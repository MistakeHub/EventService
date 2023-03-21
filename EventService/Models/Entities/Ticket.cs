namespace EventService.Models.Entities;

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
    public Guid? IdOwner { get; set; }

    /// <summary>
    /// Место
    /// </summary>
    public int? Seat { get; set; }

}