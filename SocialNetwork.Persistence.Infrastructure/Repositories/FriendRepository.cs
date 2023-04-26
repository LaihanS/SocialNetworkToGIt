using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SocialNetwork.Core.Application.Helpers;
using SocialNetwork.Core.Application.IRepositories;
using SocialNetwork.Core.Application.ViewModels.User;
using SocialNetwork.Core.Application.ViewModels.UserFriend;
using SocialNetwork.Core.Domain.Entities;
using SocialNetwork.Infrastructure.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Infrastructure.Persistence.Repositories 

{ 

public class FriendRepository : GenericRepository<User_FriendTable>, IUSer_FriendRepository
{
    public readonly ApplicationContext applicationcon;
    private readonly IHttpContextAccessor httpcontext;
    private readonly IMapper imapper;
    UserViewModel user;
    public FriendRepository(IHttpContextAccessor httpcontext, IMapper imapper, ApplicationContext context) : base(context)
    {
        this.imapper = imapper;
        this.httpcontext = httpcontext;
        applicationcon = context;
        this.user = httpcontext.HttpContext.Session.Get<UserViewModel>("user");
    }

    public virtual async Task<List<UserFriendViewModel>> GetAsyncWithJoinNoGeneric(List<string> navProperties)
    {

        //var entryPoint = applicationcon.Usuarios
        //            .Include(amigos => amigos.Publicaciones.Select(publi => publi.Comentarios))
        //            .ToList();

        var amigos = await applicationcon.Amigos
       .Include(p => p.Usuario)
       .ThenInclude(b => b.Publicaciones)
       .ThenInclude(b => b.Comentarios)
       .Where(b => b.idUser == user.id)
       .ToListAsync();
        List<UserFriendViewModel> users = imapper.Map<List<UserFriendViewModel>>(amigos);
        return users;
    }


    public async Task<UserFriendViewModel> ValidateifFriendExists(int iduser, int idadmigo)
    {
        //User_FriendTable friend = await applicationcon.Set<User_FriendTable>().
        // FirstOrDefaultAsync(user => user.idAmigo == idadmigo && user.idUser == iduser);

        User_FriendTable friend = await applicationcon.Amigos.Include(friend => friend.Usuario)
           .FirstOrDefaultAsync(user => user.idAmigo == idadmigo && user.idUser == iduser);


        UserFriendViewModel users = imapper.Map<UserFriendViewModel>(friend);

        return users;
    }




}

}
