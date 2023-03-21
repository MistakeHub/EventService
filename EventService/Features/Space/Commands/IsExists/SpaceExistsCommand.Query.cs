
using MediatR;
using SC.Internship.Common.ScResult;

namespace EventService.Features.Space.Commands.IsExists;

/// <summary>
///  Команда проверки наличия пространства
/// </summary>
public class SpaceExistsCommand:IRequest<ScResult<bool>>
{
    /// <summary>
    /// Id пространства
    /// </summary>
    public Guid Id { get; set; }
}