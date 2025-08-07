using API_Managment_Courses.Models.Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API_Managment_Courses.Filters
{
    public class GlobalExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {

            var errors = new Dictionary<string, string[]>
            {
                 ["errors"] = new[] { context.Exception.Message} 
            };




            var response = new ApiResponse<object>
            {
                data = null,
                message = context.Exception.Message,
                success = false,
                errors = errors
            };


            switch (context.Exception)
            {
                case KeyNotFoundException _:
                    context.Result = new NotFoundObjectResult(response);
                    break;
                default:
                    context.Result = new ObjectResult(response); break;
            }

            context.ExceptionHandled = true;
        }


    }
}
