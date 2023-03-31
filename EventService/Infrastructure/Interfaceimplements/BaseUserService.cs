using EventService.Features.User;
using EventService.Infrastructure.Interfaces;

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
        _users = new List<User> { new() { Id = new Guid("d23e79bb-0ccb-4f24-a6e9-2480cc7a179d"), Nickname = "Ольга" }, new() { Id = new Guid("d23e79bb-0ccb-4f24-a6e9-2480cc7a179d"), Nickname = "Гавриил" }, new() { Id = new Guid("aec7a486-eef4-43fc-88d2-f9b82a6b2ff2"), Nickname = "Никита" }, new() { Id = new Guid("55e63ae2-7669-4f8f-9756-67a5cc99973b"), Nickname = "Сергей" } };

    }
    /// <summary>
    /// Проверка наличия пользователя
    /// </summary>

    // ReSharper disable once IdentifierTypo
    public Task<bool> IsUserExists(Guid iduser)
    {
        return Task.FromResult( _users.Any(v => v.Id == iduser));
    }
}