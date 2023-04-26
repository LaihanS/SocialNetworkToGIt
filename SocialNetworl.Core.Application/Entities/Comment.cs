using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Domain.Entities
{
    public class Comment
    {
        public int id { get; set; }
        public string? text { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; } 
        public string? ImgPath { get; set; }
        public Usuario Usuario { get; set; }

        public Publicacion Publicacion { get; set; }
    }
}
