using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SocialNetwork.Core.Application.IRepositories;
using SocialNetwork.Core.Application.IServices;
using SocialNetwork.Core.Application.Services;
using SocialNetwork.Core.Application.ViewModels.User;
using SocialNetwork.Core.Domain.Entities;
using SocialNetwork.Core.Domain.Settings;
using SocialNetwork.Infrastructure.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Infrastructure.Shared;

public static class ServiceRegistration
{
    //El primer parámetro representa a quien se extiende,
    //y el segundo en adelante el dato que se recibe
    public static void AddSharedInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {

        services.Configure<MailSettings>(configuration.GetSection("MailSettings"));

        services.AddTransient<IEmailService, EmailService>();
       
    }
}
