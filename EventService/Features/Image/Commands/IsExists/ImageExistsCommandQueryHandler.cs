
using EventService.Models.Interfaces;
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
    private readonly IBaseImageService _baseImageService;

    /// <summary>
    /// Конструктор
    /// </summary>
    public ImageExistsCommandQueryHandler(IBaseImageService baseImageService) { _baseImageService = baseImageService; }
    /// <summary>
    /// обработчик
    /// </summary>
    public Task<ScResult<bool>> Handle(ImageExistsCommand request, CancellationToken cancellationToken)
    {
        var returnResult = new ScResult<bool>();

        var isExists =  _baseImageService.IsImageExists(request.Id);

        if (!isExists) throw new ScException("Изображение не существует");

        returnResult.Result = isExists;
    
        return Task.FromResult(returnResult);
    }
}