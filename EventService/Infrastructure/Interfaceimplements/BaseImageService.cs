using EventService.Models.Entities;
using EventService.Models.Interfaces;


namespace EventService.Infrastructure.InterfaceImplements;

/// <summary>
/// Сервис для работы с изображениями
/// </summary>
public class BaseImageService : IBaseImageService
{
    private readonly List<Image> _images;
    /// <summary>
    /// Конструктор
    /// </summary>
    public BaseImageService() { _images = new List<Image> { new() { Id = new Guid("7febf16f-651b-43b0-a5e3-0da8da49e90d") } }; }

    /// <summary>
    /// Метод проверки наличия изображения
    /// </summary>
    // ReSharper disable once IdentifierTypo
    public bool IsImageExists(Guid idimage)
    {
        return _images.Any(v => v.Id == idimage);
    }
}