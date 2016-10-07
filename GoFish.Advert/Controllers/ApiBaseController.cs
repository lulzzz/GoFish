using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GoFish.Advert
{
    [Authorize]
    [ServiceFilter(typeof(ModelStateActionFilterAttribute))]
    public class ApiBaseController : Controller
    {
        protected string GetControllerName()
        {
            return this.ControllerContext.RouteData.Values["controller"].ToString().ToLower();
        }
    }
}