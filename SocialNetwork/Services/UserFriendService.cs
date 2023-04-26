using AutoMapper;
using Microsoft.AspNetCore.Http;
using SocialNetwork.Core.Application.Helpers;
using SocialNetwork.Core.Application.IRepositories;
using SocialNetwork.Core.Application.IServices;
using SocialNetwork.Core.Application.ViewModels.Posts;
using SocialNetwork.Core.Application.ViewModels.User;
using SocialNetwork.Core.Application.ViewModels.UserFriend;
using SocialNetwork.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Application.Services
{
    public class UserFriendService :  GenericService<UserFriendViewModel, SaveUserFriendViewModel, User_FriendTable>,  IUserFriendService
    {
        private readonly IHttpContextAccessor httpcontext;
        private readonly IUSer_FriendRepository userfriend;
        private readonly IMapper imapper;
        UserViewModel user;
        public UserFriendService(IHttpContextAccessor httpcontext, IUSer_FriendRepository userfriend, IMapper imapper): base(imapper, userfriend) 
        {
            this.userfriend = userfriend;
            this.imapper = imapper;
            this.httpcontext = httpcontext;
            this.user = httpcontext.HttpContext.Session.Get<UserViewModel>("user");
        }


        public async Task AddFriend(UserViewModel uservm)
        {
            User_FriendTable user_Friend = new();

            user_Friend.idAmigo = uservm.id;
            user_Friend.idUser = user.id;

            User_FriendTable friend = await userfriend.AddAsync(user_Friend);

        }


        public async Task<List<UserFriendViewModel>> GetFriendAsync()
        {
            var relatedlist = await userfriend.GetAsyncWithJoinNoGeneric(new List<string> {"Usuario"});

            List<UserFriendViewModel> friendlist = imapper.Map<List<UserFriendViewModel>>
                 (relatedlist).ToList();

            return friendlist;
        }

        public async Task<UserFriendViewModel> FriendExists(int iduser, int idamigo)
        {
            var relatedlist = await userfriend.ValidateifFriendExists(iduser, idamigo);

            return relatedlist;
        }



    }
}
