﻿using MediatR;
using SC.Internship.Common.ScResult;

namespace EventService.Features.Space.IsExists;

/// <summary>
///  Команда проверки наличия пространства
/// </summary>
public class SpaceExistsCommand : IRequest<ScResult<bool>>
{
    /// <summary>
    /// Id пространства
    /// </summary>
    public Guid Id { get; set; }
    /// <summary>
    /// поле для типа авторизации
    /// </summary>
    public string? Authorization { get; set; } = null!;
}