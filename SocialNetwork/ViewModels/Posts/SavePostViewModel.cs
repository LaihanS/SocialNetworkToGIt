using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using SocialNetwork.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Application.ViewModels.Posts
{
    public class SavePostViewModel
    {
        public int id { get; set; }
        public string? PostText { get; set; }

        public IFormFile? File { get; set; }

        public string? createdBy { get; set; }
        public DateTime? created { get; set; }
        public string? modifiedBy { get; set; }

        public DateTime? modifiedAt { get; set; }

        public string? ImgPath { get; set; }

        public int UserID { get; set; }
    }
}
