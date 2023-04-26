using Microsoft.AspNetCore.Http;
using SocialNetwork.Core.Application.Helpers;
using SocialNetwork.Core.Application.ViewModels.User;

namespace SocialNetwork.WebApp.ValidateSession
{
    public class ValidateSession
    {
        private readonly IHttpContextAccessor contextAccessor;
        public ValidateSession(IHttpContextAccessor contextAccessor)
        {
            this.contextAccessor = contextAccessor;
        }

        public bool isloggedIn()
        {
            UserViewModel user = contextAccessor.HttpContext.Session.Get<UserViewModel>("user");
            if (user != null)
            {
                return true;
            }

            return false;
        }
    }
}
