
using EventService.Models.Interfaces;
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
    private readonly IBaseSpaceService _baseSpaceService;

    /// <summary>
    /// Конструктор
    /// </summary>

    public SpaceExistsCommandQueryHandler(IBaseSpaceService baseSpaceService) { _baseSpaceService=baseSpaceService; }
    /// <summary>
    /// Обработчик
    /// </summary>

    public Task<ScResult<bool>> Handle(SpaceExistsCommand request, CancellationToken cancellationToken)
    {
        var returnResult = new ScResult<bool>();

        var isExists = _baseSpaceService.IsSpaceExists(request.Id);

        if (!isExists) throw new ScException("Пространства не существует");

        returnResult.Result = isExists;

        return Task.FromResult(returnResult);
    }
}