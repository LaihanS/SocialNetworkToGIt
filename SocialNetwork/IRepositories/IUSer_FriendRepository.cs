using SocialNetwork.Core.Application.ViewModels.UserFriend;
using SocialNetwork.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Application.IRepositories
{
    public interface IUSer_FriendRepository: IGenericRepository<User_FriendTable>
    {
        Task<List<UserFriendViewModel>> GetAsyncWithJoinNoGeneric(List<string> navProperties);

        Task<UserFriendViewModel> ValidateifFriendExists(int iduser, int idadmigo);

    }
}
