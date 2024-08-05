using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AutificationService
{
    public class ExceptionHandler : ActionFilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            string message = "Произошла ошибка , администрация сайта летит на оленях! ";

            if (context.Exception is CustomException)
            {
                message = context.Exception.Message;

            }

            context.Result = new BadRequestObjectResult(message);
        }
    }
}
