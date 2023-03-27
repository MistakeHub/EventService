using EventService.Models.Interfaces;
using MediatR;
using SC.Internship.Common.Exceptions;
using SC.Internship.Common.ScResult;

namespace EventService.Features.User.Commands.IsExists;

// ReSharper disable once UnusedMember.Global Решарпер предлагает удалить хэндлер команды, т.к он явно нигде не используется
/// <summary>
/// Класс обработчик команды проверки наличия пользователя
/// </summary>
public class UserExistsCommandQueryHandler:IRequestHandler<UserExistsCommand, ScResult<bool>>
{
    private readonly IBaseUserService _baseUserService;

    /// <summary>
    /// Конструктор
    /// </summary>
    public UserExistsCommandQueryHandler(IBaseUserService baseUserService)
    {
        _baseUserService = baseUserService;
    }

    /// <summary>
    /// Обработчик
    /// </summary>
    public  Task<ScResult<bool>> Handle(UserExistsCommand request, CancellationToken cancellationToken)
    {
        var returnResult = new ScResult<bool>();

        var isUserExists = _baseUserService.IsUserExists(request.Id);

        if(!isUserExists) throw new ScException("Такого пользователя не существует");

        returnResult.Result = isUserExists;

        return Task.FromResult(returnResult);
    }
}