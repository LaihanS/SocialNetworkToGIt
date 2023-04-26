using SocialNetwork.Core.Application.ViewModels.Posts;
using SocialNetwork.Core.Application.ViewModels.User;
using SocialNetwork.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Application.ViewModels.Comentarios
{
    public class CommentViewModel
    {
        public int id { get; set; }
        public string text { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }

        public string? ImgPath { get; set; }
        public UserViewModel Usuario { get; set; }

        public PostViewModel Publicacion { get; set; }
    }
}
