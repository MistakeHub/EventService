using MediatR;
using SC.Internship.Common.ScResult;

namespace EventService.Features.User.Commands.IsExists;

/// <summary>
/// Команда проверки наличия пользователя
/// </summary>
public class UserExistsCommand:IRequest<ScResult<bool>>
{
    /// <summary>
    /// Id пользователя
    /// </summary>
    public Guid Id { get; set; }
}