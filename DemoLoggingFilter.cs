using Microsoft.AspNetCore.Mvc.Filters;

namespace demo
{

    // Need to inherent from IActionFilter
    public class DemoLoggingFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine("Action has been executed");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine("Action is executing");
        }
    }
}
