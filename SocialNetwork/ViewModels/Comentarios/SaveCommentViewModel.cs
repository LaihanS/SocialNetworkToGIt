using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Application.ViewModels.Comentarios
{
    public class SaveCommentViewModel
    {
        public int id { get; set; }
        public string text { get; set; }
        public string? ImgPath { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }
    }
}
