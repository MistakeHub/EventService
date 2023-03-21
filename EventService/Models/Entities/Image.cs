namespace EventService.Models.Entities;

/// <summary>
/// Класс изображения
/// </summary>
public class Image
{
    /// <summary>
    /// Id изображения
    /// </summary>
    public Guid Id { get; set; }=Guid.NewGuid();

}