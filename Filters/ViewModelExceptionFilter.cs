using GestaoDeResiduos.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace GestaoDeResiduos.Filters
{
    public class ViewModelExceptionFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState
                    .Where(e => e.Value.Errors.Count > 0)
                    .Select(e => new
                    {
                        Name = e.Key,
                        Messages = e.Value.Errors.Select(err => err.ErrorMessage).ToArray()
                    }).ToArray();

                var apiResponse = new BaseApiResponse<object>("Não foi possível processar a requisição.", errors);
                context.Result = new BadRequestObjectResult(apiResponse);
            }
        }

        public void OnActionExecuted(ActionExecutedContext context) { }
    }
}