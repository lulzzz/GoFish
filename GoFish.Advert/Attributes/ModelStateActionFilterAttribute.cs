using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GoFish.Advert
{
    public class ModelStateActionFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // Allow for overloaded GET for potential querystrings being sent
            if (context.HttpContext.Request.Method == "GET"
                && context.HttpContext.Request.QueryString.HasValue
                && !context.ModelState.IsValid)
                context.Result = new BadRequestObjectResult(context.ModelState);

            // Ensure posted values conform to the Model required
            if ((context.HttpContext.Request.Method == "POST" || context.HttpContext.Request.Method == "PUT")
                && !context.ModelState.IsValid)
                context.Result = new BadRequestObjectResult(context.ModelState);
        }
    }
}