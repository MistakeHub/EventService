namespace EventService.Models.Interfaces;

/// <summary>
/// Интерфейс для сервисов пользователей
/// </summary>
public interface IBaseUserService
{
    /// <summary>
    /// проверка наличия пользователя
    /// </summary>
    /// <param name="iduser">пользователь</param>
    /// <returns>результат проверки</returns>
    public bool IsUserExists(Guid iduser);
}