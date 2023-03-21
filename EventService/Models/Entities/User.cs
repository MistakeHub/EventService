namespace EventService.Models.Entities;

/// <summary>
/// Пользователь
/// </summary>
public class User
{
    /// <summary>
    /// Id пользователя
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Ник пользователя
    /// </summary>
    public string? Nickname { get; set; }
}