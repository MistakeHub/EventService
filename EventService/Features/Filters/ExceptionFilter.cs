
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SC.Internship.Common.Exceptions;
using SC.Internship.Common.ScResult;


namespace EventService.Features.Filters;

/// <summary>
/// Фильтр обработки исключений 
/// </summary>
public class ExceptionFilter :ActionFilterAttribute, IExceptionFilter

{

    /// <summary>
    /// Обработчик
    /// </summary>
    /// <param name="context"></param>
    public void OnException(ExceptionContext context)
    {
        var exception=context.Exception;
        ScError scError;
        if (exception is ScException)
        {
            
            if (exception.InnerException is ValidationException validException)
            {
                scError = new ScError
                    { Message = exception.Message, ModelState = new Dictionary<string, List<string>>() };

                foreach (var error in validException.Errors.GroupBy(v=>v.PropertyName))
                {
                    scError.ModelState.Add(error.Key, new List<string>
 {
     error.Select(v=>v.ErrorMessage).FirstOrDefault() ?? string.Empty
 } );
                }
            }
            else
            {
                scError = new ScError { Message = exception.Message };
            }

            context.Result = new BadRequestObjectResult(new ScResult(scError));

        }
        else
        {
            scError = new ScError { Message = exception.Message };
            context.Result =new ObjectResult(new ScResult(scError)){StatusCode = 500};

        }




    }
}