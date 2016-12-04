using Microsoft.AspNetCore.Mvc.Filters;

namespace EmoticonDemo.ActionFilter
{
    public class EmoticonFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var response = context.HttpContext.Response;

            response.Body = new EmoticonStream(response.Body);
        }
    }
}