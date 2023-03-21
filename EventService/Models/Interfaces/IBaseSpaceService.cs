

namespace EventService.Models.Interfaces;

/// <summary>
/// Интерфейс для сервисов пространств
/// </summary>
public interface IBaseSpaceService
{
    /// <summary>
    /// проверка наличия пространства
    /// </summary>
    /// <param name="idspace">пространство</param>
    /// <returns>результат проверки</returns>
    public bool IsSpaceExists(Guid idspace);
}