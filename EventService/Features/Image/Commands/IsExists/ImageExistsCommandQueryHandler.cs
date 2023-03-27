

using EventService.ObjectStorage.HttpService;
using MediatR;
using SC.Internship.Common.Exceptions;
using SC.Internship.Common.ScResult;

namespace EventService.Features.Image.Commands.IsExists;

// ReSharper disable once UnusedMember.Global Решарпер рекомендует удалить, так как он не используется
/// <summary>
/// Класс обработчик команды проверки наличия изображения
/// </summary>
public class ImageExistsCommandQueryHandler:IRequestHandler<ImageExistsCommand, ScResult<bool>>
{
  
    private readonly HttpServiceClient _httpServiceClient;

    /// <summary>
    /// Конструктор
    /// </summary>
    public ImageExistsCommandQueryHandler(HttpServiceClient httpServiceClient)
    {
        _httpServiceClient = httpServiceClient;

    }
    /// <summary>
    /// обработчик
    /// </summary>
    public async Task<ScResult<bool>> Handle(ImageExistsCommand request, CancellationToken cancellationToken)
    {

        var isExists = await _httpServiceClient.SendRequest<ScResult<bool>>
            ("image",$"/isimageexists/{request.Id}", "Get",null!, request.Authorization!);

        if (!isExists.Result) throw new ScException("Изображение не существует");

        return isExists;
    }
}