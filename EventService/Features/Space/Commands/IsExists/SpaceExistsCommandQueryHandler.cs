

using EventService.ObjectStorage.HttpService;
using MediatR;
using SC.Internship.Common.Exceptions;
using SC.Internship.Common.ScResult;

namespace EventService.Features.Space.Commands.IsExists;

// ReSharper disable once UnusedMember.Global Решарпер предлагает удалить хэндлер команды, т.к он явно нигде не используется
/// <summary>
/// Класс обработчик команды проверки наличия изображения
/// </summary>
public class SpaceExistsCommandQueryHandler : IRequestHandler<SpaceExistsCommand, ScResult<bool>>
{
   
    private readonly HttpServiceClient _httpServiceClient;

    /// <summary>
    /// Конструктор
    /// </summary>

    public SpaceExistsCommandQueryHandler(HttpServiceClient httpServiceClient)
    {
        _httpServiceClient = httpServiceClient;
    }
    /// <summary>
    /// Обработчик
    /// </summary>

    public async Task<ScResult<bool>> Handle(SpaceExistsCommand request, CancellationToken cancellationToken)
    {
        var isExists =await _httpServiceClient.SendRequest<ScResult<bool>>("space",$"/isspaceexists/{request.Id}", "Get",null!, request.Authorization!);

        if (!isExists.Result) throw new ScException("Пространства не существует");

        return isExists;
    }
}