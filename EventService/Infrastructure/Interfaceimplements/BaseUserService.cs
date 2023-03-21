using EventService.Models.Entities;
using EventService.Models.Interfaces;

namespace EventService.Infrastructure.InterfaceImplements;

/// <summary>
/// Сервис для работы с пользователями 
/// </summary>
public class BaseUserService : IBaseUserService
{
    private readonly List<User> _users;

    /// <summary>
    /// Конструктор
    /// </summary>
    public BaseUserService()
    {
        _users = new List<User> { new() { Id = new Guid("7fsdf16f-91eb-32b5-a5e3-0da8da49e90d"), Nickname = "Ольга" }, new() { Id = new Guid("7fsdf16f-91eb-32b5-a5e3-0da8da49e91d"), Nickname = "Гавриил" }, new() { Id = new Guid("7fsdf16f-91eb-32b5-a5e3-0da8da49e92d"), Nickname = "Никита" }, new() { Id = new Guid("7fsdf16f-91eb-32b5-a5e3-0da8da49e93d"), Nickname = "Сергей" } };

    }
    /// <summary>
    /// Проверка наличия пользователя
    /// </summary>

    // ReSharper disable once IdentifierTypo
    public bool IsUserExists(Guid iduser)
    {
        return _users.Any(v => v.Id == iduser);
    }
}