using SocialNetwork.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Domain.Entities
{
    public class Publicacion: AuditableBaseEntity
    {
        public string? PostText { get; set; }

        public string? ImgPath { get; set; }

        public int UserID { get; set; }

        public Usuario Usuario { get; set; }

        public List<Comment> Comentarios { get; set; }

    }
}   
