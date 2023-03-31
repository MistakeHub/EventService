namespace EventService.Infrastructure.Interfaces;

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
    public Task<bool> IsUserExists(Guid iduser);
}