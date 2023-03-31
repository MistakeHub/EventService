
using FluentValidation;

using MediatR;
using SC.Internship.Common.Exceptions;

namespace EventService.Features.Filters;

/// <inheritdoc />
public class ValidationBehavior<TRequest, TResponse>:IPipelineBehavior<TRequest, TResponse> where TRequest: class, IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="validators"></param>
    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators) => _validators = validators;
    /// <summary>
    /// Хэндлер
    /// </summary>
 
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!_validators.Any())
        {
            return await next();
        }
        var context = new ValidationContext<TRequest>(request);
        var validationFailures = _validators
            .Select(x => x.Validate(context))
            .SelectMany(x => x.Errors).ToList();

           
        if (validationFailures.Any())
        {
            throw new ScException( new ValidationException(validationFailures), "ValidationException");
        }
        return await next();
    }
}