using SocialNetwork.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Application.ViewModels.UserFriend
{
    public class UserFriendViewModel
    {
        public int id { get; set; }

        public int idUser { get; set; }
        public int idAmigo { get; set; }

        public Usuario Usuario { get; set; }

        //public ICollection<Comment> comentarios { get; set; }
        //public ICollection<Publicacion> publicacions { get; set; }

    }
}
