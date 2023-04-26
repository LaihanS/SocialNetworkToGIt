using SocialNetwork.Core.Application.ViewModels.Posts;
using SocialNetwork.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Application.IServices
{
    public interface IPostService : IGenericService<PostViewModel, SavePostViewModel, Publicacion> 
    {
        Task<List<PostViewModel>> GetAsyncJoin();
    }
}
