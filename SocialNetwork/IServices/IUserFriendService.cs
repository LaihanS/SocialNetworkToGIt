using SocialNetwork.Core.Application.ViewModels.User;
using SocialNetwork.Core.Application.ViewModels.UserFriend;
using SocialNetwork.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Application.IServices
{
    public interface IUserFriendService: IGenericService<UserFriendViewModel, SaveUserFriendViewModel, User_FriendTable>
    {
        Task AddFriend(UserViewModel uservm);

        Task<List<UserFriendViewModel>> GetFriendAsync();

        Task<UserFriendViewModel> FriendExists(int iduser, int idamigo);
    }
}
