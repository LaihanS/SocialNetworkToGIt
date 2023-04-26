using AutoMapper;
using Microsoft.AspNetCore.Http;
using SocialNetwork.Core.Application.IRepositories;
using SocialNetwork.Core.Application.IServices;
using SocialNetwork.Core.Application.ViewModels.Comentarios;
using SocialNetwork.Core.Application.ViewModels.Posts;
using SocialNetwork.Core.Application.ViewModels.User;
using SocialNetwork.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using SocialNetwork.Core.Application.ViewModels.User;
using SocialNetwork.Core.Application.Helpers;

//@{
//   
//}

namespace SocialNetwork.Core.Application.Services
{
    public class PostService : GenericService<PostViewModel, SavePostViewModel, Publicacion>, IPostService
    {
        private readonly IHttpContextAccessor httpcontext;
        private readonly IPostRepository postRepository;
        private readonly IMapper imapper;
        UserViewModel user;
        public PostService(IHttpContextAccessor httpcontext, IPostRepository postRepository, IMapper imapper) : base(imapper, postRepository)
        {
            this.postRepository = postRepository;
            this.imapper = imapper;
            this.httpcontext = httpcontext;
            this.user = httpcontext.HttpContext.Session.Get<UserViewModel>("user");
        }

        public async Task<List<PostViewModel>> GetAsyncJoin()
        {
            var post = await postRepository.GetAsyncWithJoin(new List<string> {"Comentarios", "Usuario" });
            List<PostViewModel> listpost = post.Select(publi => new PostViewModel
            {
                created = publi.created,
                id = publi.id,
                PostText = publi.PostText,
                ImgPath = publi.ImgPath,
                UserID = publi.UserID,
                Usuario = imapper.Map<UserViewModel>(publi.Usuario),
                Comentarios = imapper.Map<List<CommentViewModel>>(publi.Comentarios),
            
            }).ToList();

            listpost = listpost.Where(post => post.UserID == user.id).ToList();
            return listpost;
        }


    }
}
