using SocialNetwork.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Domain.Entities
{
    public class Usuario: AuditableBaseEntity
    {
        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public string Telefono { get; set; }

        public string? ImgPath { get; set; }

        public string Correo { get; set; }

        public string UserName { get; set; }
        public string Contraseña { get; set; }

        public bool Activo { get; set; }

        public ICollection<Publicacion> Publicaciones { get; set; }

        public ICollection<Comment> Comentarios { get; set; }

        public ICollection<User_FriendTable> Amigos{ get; set; }

    }
}
