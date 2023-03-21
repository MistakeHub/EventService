using EventService.Models.Entities;
using EventService.Models.Interfaces;

namespace EventService.Infrastructure.InterfaceImplements;

/// <summary>
/// Сервис работы с пространствами
/// </summary>
public class BaseSpaceService : IBaseSpaceService
{
    private readonly List<Space> _spaces;

    /// <summary>
    /// Конструктор
    /// </summary>
    public BaseSpaceService() { _spaces = new List<Space> { new() { Id = new Guid("7febf16f-651b-43b0-a5e3-0da8da49e90d"), Name = "Пространство 1" } }; }


    /// <summary>
    /// Метод проверки наличия пространства
    /// </summary>

    // ReSharper disable once IdentifierTypo
    public bool IsSpaceExists(Guid idspace)
    {
        return _spaces.Any(v => v.Id == idspace);
    }


}