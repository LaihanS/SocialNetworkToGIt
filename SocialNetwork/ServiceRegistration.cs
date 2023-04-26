using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SocialNetwork.Core.Application.IRepositories;
using SocialNetwork.Core.Application.IServices;
using SocialNetwork.Core.Application.Services;
using SocialNetwork.Core.Application.ViewModels.User;
using SocialNetwork.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Application;

public static class ServiceRegistration
{
    //El primer parámetro representa a quien se extiende,
    //y el segundo en adelante el dato que se recibe
    public static void AddPersistenceInfrastructure(this IServiceCollection services)
    {

        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        #region ServicesDependency
        //services.AddTransient<IGenericService, GenericService>();
        services.AddTransient(typeof(IGenericService<,,>), typeof(GenericService<,,>));
        services.AddTransient<IUserService, UserService>();
        services.AddTransient<IPostService, PostService>();
        services.AddTransient<ICommentService, CommentService>();
        services.AddTransient<IUserFriendService, UserFriendService>();
        #endregion
    }
}
