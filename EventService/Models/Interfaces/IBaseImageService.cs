

namespace EventService.Models.Interfaces;

/// <summary>
/// Интерфейс для сервисов изображений
/// </summary>
public interface IBaseImageService
{

    /// <summary>
    /// наличие изображения
    /// </summary>
    /// <param name="idimage">изображение</param>
    /// <returns>результат проверки</returns>
    public bool IsImageExists(Guid idimage);

}