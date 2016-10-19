using System;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace GoFish.UI.MVC.Shared
{
    public class UserDetails : IUserDetails
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public UserDetails(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public int GetUserId()
        {
            var userClaim = _contextAccessor.HttpContext.User.Claims.Where(u => u.Type == "sub").SingleOrDefault();
            if (userClaim == null)
                throw new InvalidOperationException("UserId cannot be read");

            return int.Parse(userClaim.Value);
        }

        public string GetUserName()
        {
            var userClaim = _contextAccessor.HttpContext.User.Claims.Where(u => u.Type == "name").SingleOrDefault();
            return userClaim == null ? "" : userClaim.Value;
        }
    }
}
