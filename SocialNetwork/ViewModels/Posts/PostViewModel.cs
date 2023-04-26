using SocialNetwork.Core.Application.ViewModels.Comentarios;
using SocialNetwork.Core.Application.ViewModels.User;
using SocialNetwork.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Application.ViewModels.Posts
{
    public class PostViewModel
    {
        public int id { get; set; }
        public string PostText { get; set; }

        public string? createdBy { get; set; }
        public DateTime? created { get; set; }
        public string? modifiedBy { get; set; }

        public DateTime? modifiedAt { get; set; }

        public string File { get; set; }

        public string? ImgPath { get; set; }

        public int UserID { get; set; }

        public UserViewModel? Usuario { get; set; }

        public List<CommentViewModel>? Comentarios { get; set; }
    }
}
