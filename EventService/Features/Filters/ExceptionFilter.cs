
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SC.Internship.Common.Exceptions;
using SC.Internship.Common.ScResult;

namespace EventService.Features.Filters
{
    public class ExceptionFilter :ActionFilterAttribute, IExceptionFilter

    {

        public void OnException(ExceptionContext context)
        {
            var exception=context.Exception;
            if (exception is ScException)
            {
                
                var scException= context.Exception as ScException;
                ScError scError;
                if (exception.InnerException is ValidationException validException)
                {
                    scError = new ScError() { Message = scException.Message, ModelState = new Dictionary<string, List<string>>() };
                     
                    foreach (var error in validException.Errors)
                    {
                        scError.ModelState.Add(error.PropertyName, new List<string>() { error.ErrorMessage });
                    }
                }
                else
                {
                    scError = new ScError() { Message = exception.Message};
                }

                context.Result = new BadRequestObjectResult(new ScResult(scError));

            }

        
         
        }
    }
}
