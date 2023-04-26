using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using SocialNerwork.Models;
using SocialNetwork.Core.Application.Helpers;
using SocialNetwork.Core.Application.IServices;
using SocialNetwork.Core.Application.Services;
using SocialNetwork.Core.Application.ViewModels.Comentarios;
using SocialNetwork.Core.Application.ViewModels.Posts;
using SocialNetwork.Core.Application.ViewModels.User;
using SocialNetwork.WebApp.Helpers;
using SocialNetwork.WebApp.ValidateSession;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;

namespace SocialNerwork.Controllers
{
    public class HomeController : Controller
    {
        
        private readonly IHttpContextAccessor contextAccessor;
        private readonly IPostService postservice;
        private readonly ICommentService commentService;
        private readonly IMapper Imapper;
        private readonly  IsLoggedIn isLogged;
        private readonly ReturnPostList postList;

        UserViewModel user;
        public HomeController(ReturnPostList postList, IsLoggedIn isLogged, IMapper Imapper, ICommentService commentService, IPostService postservice, IHttpContextAccessor contextAccessor)
        {
            this.contextAccessor = contextAccessor;
            this.postservice = postservice;
            this.commentService = commentService;
            this.isLogged = isLogged;
            this.postList = postList;
            this.Imapper = Imapper;
            user = contextAccessor.HttpContext.Session.Get<UserViewModel>("user");
        }
        public async Task<IActionResult> Index(SaveCommentViewModel comment)
        {
            if (!isLogged.isloggedIn()) 
            {
                return RedirectToRoute(new { controller = "Usuario", action = "Index" });
            }
           
            if (comment.text != null && comment.ImgPath != null)
            {
                SaveCommentViewModel commenta = await commentService.AddAsync(comment);
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }

            ViewBag.Post = postList.SortPostList(await postservice.GetAsyncJoin());
            return View("Index" ,new SavePostViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost(SavePostViewModel savepostvm)
        {
            if (!isLogged.isloggedIn())
            {
                return RedirectToRoute(new { controller = "Usuario", action = "Index" });
            }

            if (savepostvm.File == null && savepostvm.PostText == null)
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }

            if(savepostvm.File != null || savepostvm.PostText != null)
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.Post = postList.SortPostList(await postservice.GetAsyncJoin());
                    return View("Index", savepostvm);
                }

                SavePostViewModel post = await postservice.AddAsync(savepostvm);

                if (post != null && post.id != null)
                {
                    post.ImgPath = ReturnImageUrl(savepostvm.File, post.id);
                    await postservice.EditAsync(post, post.id);
                }
            }

            return RedirectToRoute(new { controller = "Home", action = "Index" });

        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int postID)
        {
            if (!isLogged.isloggedIn())
            {
                return RedirectToRoute(new { controller = "Usuario", action = "Index" });
            }

            SavePostViewModel post = await postservice.GetEditAsync(postID);

           await postservice.Delete(post, post.id);

            string basepath = $"/Images/Post/{post.id}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basepath}");
            if (Directory.Exists(path))
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(path);
                foreach (FileInfo file in directoryInfo.GetFiles())
                {
                    file.Delete();
                }

                foreach (DirectoryInfo directory in directoryInfo.GetDirectories())
                {
                    directory.Delete();
                }

                Directory.Delete(path);
            }

            return RedirectToRoute(new { controller = "Home", action = "Index" });
        }

       
        public async Task<IActionResult> EditPost(int postID)
        {
            if (!isLogged.isloggedIn())
            {
                return RedirectToRoute(new { controller = "Usuario", action = "Index" });
            }

            SavePostViewModel post = await postservice.GetEditAsync(postID);

            return View("EditPost", post);
        }

        [HttpPost]
        public async Task<IActionResult> EditPost(SavePostViewModel post)
        {
            if (!isLogged.isloggedIn())
            {
                return RedirectToRoute(new { controller = "Usuario", action = "Index" });
            }

            SavePostViewModel posteo = await postservice.GetEditAsync(post.id);
            posteo.ImgPath = ReturnImageUrl(post.File, posteo.id, posteo.ImgPath, true);
            posteo.PostText = post.PostText;

            await postservice.EditAsync(posteo, post.id);

            return RedirectToRoute(new { controller = "Home", action = "Index" });
        }


        private string ReturnImageUrl(IFormFile file, int id, string url = "", bool editmode = false)
        {
            if (editmode && file == null)
            {
                return url;
            }

            
            if (file == null)
             {
                return "";
             }

            //Crear directorio para la imagen actual
            string basepath = $"/Images/Post/{id}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basepath}");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            //Crear la ruta de la imagen actual
            Guid guid = Guid.NewGuid();
            FileInfo fileInfo = new(file.FileName);
            string filename = guid + fileInfo.Extension;

            string filePath = Path.Combine(path, filename);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            if (editmode && url != null)
            {
                string[] oldpath = default;
                oldpath = url.Split("/");

                string odlImgageName = oldpath[^1];
                string OldfilePath = Path.Combine(path, odlImgageName);

                if (System.IO.File.Exists(OldfilePath))
                {
                 System.IO.File.Delete(OldfilePath);
                }    
               
            }

            return $"{basepath}/{filename}";
        }
    }

}