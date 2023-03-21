namespace EventService.Models.Entities;

/// <summary>
/// Пространство
/// </summary>
public class Space
{
    /// <summary>
    /// Id пространства
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Название пространства
    /// </summary>
    public string Name { get; set; } = null!;


}