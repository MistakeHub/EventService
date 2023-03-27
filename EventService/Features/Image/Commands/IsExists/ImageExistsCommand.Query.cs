
using MediatR;
using SC.Internship.Common.ScResult;

namespace EventService.Features.Image.Commands.IsExists;

/// <summary>
/// Команда проверки наличия изображения
/// </summary>
public class ImageExistsCommand:IRequest<ScResult<bool>>
{
    /// <summary>
    /// Id изображения
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// поле для метода авторизации
    /// </summary>
    public string? Authorization { get; set; } = null!;
}