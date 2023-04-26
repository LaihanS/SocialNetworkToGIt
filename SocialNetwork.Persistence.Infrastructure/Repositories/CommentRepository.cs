using SocialNetwork.Core.Application.IRepositories;
using SocialNetwork.Core.Domain.Entities;
using SocialNetwork.Infrastructure.Persistence.Contexts;
using SocialNetwork.Infrastructure.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Infrastructure.Persistence.Repositories
{
    public class CommentRepository: GenericRepository<Comment>, ICommentRepository
    {
        public readonly ApplicationContext applicationcon;
        public CommentRepository(ApplicationContext context) : base(context)
        {
            applicationcon = context;
        }

    }
}
