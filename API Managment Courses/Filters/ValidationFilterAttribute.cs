using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using API_Managment_Courses.Models.Api;
namespace API_Managment_Courses.Filters
{
    public class ValidationFilterAttribute : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var errors = context.ModelState
                        .Where(kvp => kvp.Value.Errors.Any())
                        .ToDictionary(
                            kvp => kvp.Key,
                            kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                         );



            if (!context.ModelState.IsValid) {
                context.Result = new UnprocessableEntityObjectResult(new ApiResponse<object>
                {
                    data = null,
                    success = false,
                    message = "error",
                    errors = errors
                });
            }
        }

        public void OnActionExecuted(ActionExecutedContext context) { }

    }
}
