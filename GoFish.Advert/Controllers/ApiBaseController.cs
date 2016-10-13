using System.Linq;
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

        protected int GetUserId()
        {
            var userIdClaim = HttpContext.User.Claims.Where(t => t.Type == "sub").SingleOrDefault();
            return userIdClaim == null ? 1 : int.Parse(userIdClaim.Value); // Hack for postman testing
        }
    }
}