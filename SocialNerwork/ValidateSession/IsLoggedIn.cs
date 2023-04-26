using Microsoft.AspNetCore.Http;
using SocialNetwork.Core.Application.Helpers;
using SocialNetwork.Core.Application.ViewModels.User;

namespace SocialNetwork.WebApp.ValidateSession
{
    public class IsLoggedIn
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public IsLoggedIn(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public bool isloggedIn()
        {
            UserViewModel user = _contextAccessor.HttpContext.Session.Get<UserViewModel>("user");
            if (user != null)
            {
                return true;
            }

            return false;
        }
    }
}
