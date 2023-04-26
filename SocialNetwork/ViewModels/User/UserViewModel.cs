using SocialNetwork.Core.Application.ViewModels.Comentarios;
using SocialNetwork.Core.Application.ViewModels.Posts;
using SocialNetwork.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Application.ViewModels.User
{
    public class UserViewModel
    {
        public int id { get; set; }
        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public string Telefono { get; set; }

        public string file { get; set; }

        public string ImgPath { get; set; }
        public string Correo { get; set; }

        public string Contraseña { get; set; }

        public string UserName { get; set; }

        public bool Activo { get; set; }

        public List<PostViewModel>? Publicaciones { get; set; }

        public List<CommentViewModel>? Comentarios { get; set; }

        public ICollection<User_FriendTable> Amigos { get; set; }
    }
}
