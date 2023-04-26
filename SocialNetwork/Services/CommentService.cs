using AutoMapper;
using SocialNetwork.Core.Application.IRepositories;
using SocialNetwork.Core.Application.IServices;
using SocialNetwork.Core.Application.ViewModels.Comentarios;
using SocialNetwork.Core.Application.ViewModels.Posts;
using SocialNetwork.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Application.Services
{
    public class CommentService : GenericService<CommentViewModel, SaveCommentViewModel, Comment>, ICommentService
    {
        private readonly ICommentRepository commentRepository;
        private readonly IMapper imapper;
        public CommentService(ICommentRepository commentRepository, IMapper imapper) : base(imapper, commentRepository)
        {
            this.commentRepository = commentRepository;
            this.imapper = imapper;
        }

        //public async Task AddComment(CommentViewModel commentvm)
        //{
        //    Comment comment = imapper.Map<Comment>(commentvm);
        //    commentRepository.AddAsync(comment);
        //}

    }
}
