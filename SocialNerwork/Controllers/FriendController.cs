using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Core.Application.Helpers;
using SocialNetwork.Core.Application.IServices;
using SocialNetwork.Core.Application.ViewModels.Comentarios;
using SocialNetwork.Core.Application.ViewModels.Posts;
using SocialNetwork.Core.Application.ViewModels.User;
using SocialNetwork.Core.Application.ViewModels.UserFriend;
using SocialNetwork.Core.Domain.Entities;
using SocialNetwork.WebApp.Helpers;
using SocialNetwork.WebApp.ValidateSession;
using System.Collections.Generic;

namespace SocialNetwork.WebApp.Controllers
{
    public class FriendController : Controller
    {
        private readonly IHttpContextAccessor contextAccessor;
        private readonly IUserService userService;
        private readonly IUserFriendService userFriendService;
        private readonly IMapper imapper;
        private readonly ReturnPostList validate;
        private readonly IsLoggedIn isLogged;
        private readonly ICommentService commentService;
        UserViewModel user;
        public FriendController(IsLoggedIn isLogged, ICommentService commentService, ReturnPostList validate, IMapper imapper, IUserFriendService userFriendService, IUserService userService, IHttpContextAccessor contextAccessor)
        {
            this.imapper = imapper;
            this.commentService = commentService;
            this.contextAccessor = contextAccessor;
            this.userService = userService;
            this.isLogged = isLogged;
            this.userFriendService = userFriendService;
            this.validate = validate;
            this.user = contextAccessor.HttpContext.Session.Get<UserViewModel>("user");
        }

        public async Task<IActionResult> Index()
        {
            if (!isLogged.isloggedIn())
            {
                return RedirectToRoute(new { controller = "Usuario", action = "Index" });
            }

            List<UserFriendViewModel> users = await userFriendService.GetFriendAsync();

            List<UserViewModel> uservm = imapper.Map<List<UserViewModel>>(users.Select(userp => userp.Usuario).Where(user => user != null));

            ViewBag.Usuarios = uservm;
            ViewBag.Posts = validate.returnPostList(uservm);
            return View(new UserViewModel());

        }

        [HttpPost]
        public async Task<IActionResult> Index(string username, SaveCommentViewModel comment)
        {
            if (!isLogged.isloggedIn())
            {
                return RedirectToRoute(new { controller = "Usuario", action = "Index" });
            }


            if (comment.text != null && comment.ImgPath != null)
            {
                SaveCommentViewModel commenta = await commentService.AddAsync(comment);
                return RedirectToRoute(new { controller = "Friend", action = "Index" });
            }


            UserViewModel usuariofriend = await userService.ValidateNameAsync(username);

           
            if (/*user == null ||*/ usuariofriend == null) {

                ModelState.AddModelError("userValidation", "Usuario no encontrado: Introduzca un username válido");

                List<UserFriendViewModel> users = await userFriendService.GetFriendAsync();

                List<UserViewModel> uservm = imapper.Map<List<UserViewModel>>(users.Select(userp => userp.Usuario).Where(user => user != null));

                ViewBag.Usuarios = uservm;
                ViewBag.Posts = validate.returnPostList(uservm);
                return View("Index", new UserViewModel());
            }

            if (await userFriendService.FriendExists(user.id, usuariofriend.id) != null || user.id == usuariofriend.id)
            {
                return RedirectToRoute(new { controller = "Friend", action = "Index" });
            }

    


            await userFriendService.AddFriend(usuariofriend);
            return RedirectToRoute(new { controller = "Friend", action = "Index" });
        }


        public async Task<IActionResult> Delete(int id)
        {

            if (!isLogged.isloggedIn())
            {
                return RedirectToRoute(new { controller = "Usuario", action = "Index" });
            }

            return View("DeleteFriend", await userFriendService.FriendExists(user.id, id));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UserFriendViewModel vm)
        {

            if (!isLogged.isloggedIn())
            {
                return RedirectToRoute(new { controller = "Usuario", action = "Index" });
            }

            SaveUserFriendViewModel usuario = imapper.Map<SaveUserFriendViewModel>(await userFriendService.FriendExists(user.id, vm.id));
            await userFriendService.Delete(usuario, usuario.id);
            return RedirectToRoute(new { controller = "Friend", action = "Index" });
        }
    }
}

