using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GoFish.Inventory
{
    [Authorize]
    public abstract class InventoryController : Controller
    {
        protected int GetUserId()
        {
            var userIdClaim = HttpContext.User.Claims.Where(t => t.Type == "sub").SingleOrDefault();
            return userIdClaim == null ? 7874 : int.Parse(userIdClaim.Value); // Hack for postman testing
        }
    }
}