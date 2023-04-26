using SocialNetwork.Core.Application.ViewModels.Comentarios;
using SocialNetwork.Core.Application.ViewModels.Posts;
using SocialNetwork.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Application.IServices
{
    public interface ICommentService : IGenericService<CommentViewModel, SaveCommentViewModel, Comment>
    {

    }
}
