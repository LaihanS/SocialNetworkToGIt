using AutoMapper;
using SocialNetwork.Core.Application.ViewModels.Comentarios;
using SocialNetwork.Core.Application.ViewModels.Posts;
using SocialNetwork.Core.Application.ViewModels.User;
using SocialNetwork.Core.Application.ViewModels.UserFriend;
using SocialNetwork.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Application.Mappings
{
    public class GeneralProfile: Profile
    {
        public GeneralProfile() {

            #region User-UserViewModels
            CreateMap<Usuario, UserViewModel>().ForMember(uservm => uservm.file, action => action.Ignore()).
                ReverseMap()
                .ForMember(user => user.created, action => action.Ignore())
                 .ForMember(user => user.createdBy, action => action.Ignore())
                  .ForMember(user => user.modifiedBy, action => action.Ignore())
                   .ForMember(user => user.modifiedAt, action => action.Ignore());

            CreateMap<Usuario, SaveUserViewModel>()
                .ForMember(saveuservm => saveuservm.file, action => action.Ignore())
                 .ForMember(saveuservm => saveuservm.ConfirmContraseña, action => action.Ignore()).ReverseMap()
                 .ForMember(user => user.created, action => action.Ignore())
                 .ForMember(user => user.createdBy, action => action.Ignore())
                  .ForMember(user => user.modifiedBy, action => action.Ignore())
                   .ForMember(user => user.modifiedAt, action => action.Ignore())
                   .ForMember(user => user.Comentarios, action => action.Ignore())
                 .ForMember(user => user.Amigos, action => action.Ignore())
                  .ForMember(user => user.Publicaciones, action => action.Ignore());


            CreateMap<SaveUserViewModel, UserViewModel>()
                   .ForMember(uservm => uservm.Comentarios, action => action.Ignore())
                 .ForMember(uservm => uservm.Amigos, action => action.Ignore())
                  .ForMember(uservm => uservm.Publicaciones, action => action.Ignore()).ReverseMap()
                    .ForMember(saveuser => saveuser.ConfirmContraseña, action => action.Ignore());

            #endregion

            #region Post-Postvms

            CreateMap<Publicacion, PostViewModel>().
                ForMember(postvm => postvm.File, action => action.Ignore()).ReverseMap();

            CreateMap<Publicacion, SavePostViewModel>()
                .ForMember(savepostvm => savepostvm.File, action => action.Ignore())
                 .ReverseMap()
                   .ForMember(user => user.Comentarios, action => action.Ignore())
                 .ForMember(user => user.Usuario, action => action.Ignore());
                 


            CreateMap<PostViewModel, SavePostViewModel>().ReverseMap()
                    .ForMember(postvm => postvm.Comentarios, action => action.Ignore())
                 .ForMember(postvm => postvm.Usuario, action => action.Ignore());

            #endregion


            #region Comment-CommentViewModels

            CreateMap<Comment, CommentViewModel>().ReverseMap();

             CreateMap<Comment, SaveCommentViewModel>().ReverseMap()
                 .ForMember(comment => comment.Publicacion, action => action.Ignore())
                       .ForMember(comment => comment.Usuario, action => action.Ignore());

            CreateMap<CommentViewModel,SaveCommentViewModel>().ReverseMap()
                 .ForMember(comment => comment.Publicacion, action => action.Ignore())
                       .ForMember(comment => comment.Usuario, action => action.Ignore());
            #endregion

            #region Friend-FriendViewModels

            CreateMap<User_FriendTable, UserFriendViewModel>()
                .ReverseMap()
                 .ForMember(user => user.created, action => action.Ignore())
                 .ForMember(user => user.createdBy, action => action.Ignore())
                  .ForMember(user => user.modifiedBy, action => action.Ignore())
                   .ForMember(user => user.modifiedAt, action => action.Ignore());

            CreateMap<User_FriendTable, SaveUserFriendViewModel>()
               .ReverseMap()
                .ForMember(user => user.created, action => action.Ignore())
                .ForMember(user => user.createdBy, action => action.Ignore())
                 .ForMember(user => user.modifiedBy, action => action.Ignore())
                  .ForMember(user => user.modifiedAt, action => action.Ignore())
                  .ForMember(user => user.Usuario, action => action.Ignore());

            CreateMap<UserFriendViewModel, SaveUserFriendViewModel>()
               .ReverseMap()
                  .ForMember(user => user.Usuario, action => action.Ignore());




            #endregion

        }

    }
}
