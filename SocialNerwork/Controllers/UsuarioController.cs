using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Core.Application.Dtos.Emails;
using SocialNetwork.Core.Application.Helpers;
using SocialNetwork.Core.Application.IServices;
using SocialNetwork.Core.Application.ViewModels.Posts;
using SocialNetwork.Core.Application.ViewModels.User;
using SocialNetwork.WebApp.ValidateSession;

namespace WebApp.SocialNerwork.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUserService userService;
        private readonly IHttpContextAccessor contextAccessor;
        private readonly IsLoggedIn isLoggedIn;
        private readonly IMapper mapper;
        private readonly IEmailService email;

        public UsuarioController(IMapper mapper, IEmailService email, IsLoggedIn isLoggedIn, IUserService userService, IHttpContextAccessor contextAccessor)
        {
            this.email = email;
            this.mapper = mapper;
           this. userService = userService;
            this.contextAccessor = contextAccessor;
            this.isLoggedIn = isLoggedIn;
        }

        public async Task<IActionResult> Index()
        {
            if (isLoggedIn.isloggedIn())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            if (isLoggedIn.isloggedIn())
            {
                HttpContext.Session.Remove("user");
                return RedirectToRoute(new { controller = "Usuario", action = "Index" });
            }

            return RedirectToRoute(new { controller = "Usuario", action = "Index" });
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel loginvm)
        {
            if (isLoggedIn.isloggedIn())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }

            if (!ModelState.IsValid)
            {
                return View(loginvm);
            }

            UserViewModel user = await userService.LoginAsync(loginvm);

            if (user == null)
            {
                ModelState.AddModelError("userValidation", "No existe un usuario con esos datos");
                return View(loginvm);
            }

            if (!user.Activo)
            {
                ModelState.AddModelError("userValidation", "Este usuario no se encuentra activo");
                return View(loginvm);

            }

            if (user != null)
            {
               contextAccessor.HttpContext.Session.Set<UserViewModel>("user", user);
                return RedirectToRoute(new { controller = "Home",  action = "Index" });
            }

            
            return View();
        }

        public async Task<IActionResult> Register()
        {
            if (isLoggedIn.isloggedIn())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }

            return View("Register", new SaveUserViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Register(SaveUserViewModel saveuservm)
        {
            if (isLoggedIn.isloggedIn())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }

            if (!ModelState.IsValid)
            {
                return View("Register", saveuservm);
            }

            if (await userService.Exists(saveuservm))
            {
                ModelState.AddModelError("userValidation", "Un usuario con ese UserName ya existe");
                return View("Register", saveuservm);
            }

            SaveUserViewModel user = await userService.AddAsync(saveuservm);
            if (user != null && user.id != null)
            {
                user.ImgPath = ReturnImageUrl(saveuservm.file, user.id);
                await userService.EditAsync(user, user.id); 
            }

            string url = $"https://localhost:7167/Usuario/Activate/{user.id}";
            await email.SendAsync(new EmailRequest
            {
                To = user.Correo,
                Body = $"<html><body><h1>{user.Nombre}, Debe activar su cuenta accediendo al link</h1>" +
                       $"<p>Debe activar su cuenta dando click <a href='{url}'>Aquí</a></p>" +
                       "</body></html>",

                Subject = "Activar usuario"
            });


            return RedirectToRoute(new { controller = "Usuario", action = "Index" });
        }

        public async Task<IActionResult> SearchUser()
        {
            if (isLoggedIn.isloggedIn())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SearchUser(string username)
        {
            if (isLoggedIn.isloggedIn())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            SaveUserViewModel userchange = mapper.Map<SaveUserViewModel>(await userService.ValidateNameAsync(username));
            if (userchange != null)
            {
                return View("ChangePassword", userchange);
            }
            else
            {
             
               ModelState.AddModelError("userValidation", "No existe un usuario con ese nombre");
               return View("SearchUser");
              
            }

            return RedirectToRoute(new { controller = "Usuario", action = "Index" });
        }

        public async Task<IActionResult> ChangePassword()
        {
            if (isLoggedIn.isloggedIn())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }

            return View(new SaveUserViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(SaveUserViewModel uservm)
        {
            if (isLoggedIn.isloggedIn())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }

            SaveUserViewModel getuservm = await userService.GetEditAsync(uservm.id);
            getuservm.Contraseña = uservm.Contraseña;

           await userService.EditEncryptAsync(getuservm, getuservm.id);

            await email.SendAsync(new EmailRequest
            {
                To = getuservm.Correo,
                Body = "Contraseña cambiada exitosamente",
                Subject = $"{getuservm.Nombre}, Contraseña cambiada"
            });

            return RedirectToRoute(new { controller = "Usuario", action = "Index" });

        }

        public async Task<IActionResult> Activate(int id)
        {
            if (isLoggedIn.isloggedIn())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }

            SaveUserViewModel getuservm = await userService.GetEditAsync(id);
            
            return View(getuservm);
        }

        [HttpPost]
        public async Task<IActionResult> Astivar(SaveUserViewModel uservm)
        {
            if (isLoggedIn.isloggedIn())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }

            SaveUserViewModel getuservm = await userService.GetEditAsync(uservm.id);
            getuservm.Activo = true;
            await userService.EditAsync(getuservm, getuservm.id);

            return RedirectToRoute(new { controller = "Usuario", action = "Index" });
        }

            private string ReturnImageUrl(IFormFile file, int id, string url = "", bool editmode = false)
        {
            if (editmode && file == null)
            {
                return url;
            }

            //Crear directorio para la imagen actual
            string basepath = $"/Images/User/{id}";
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

            if (editmode)
            {
                string[] oldpath = url.Split("/");
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
