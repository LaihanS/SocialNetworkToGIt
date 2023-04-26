using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SocialNetwork.Core.Application.Helpers;
using SocialNetwork.Core.Application.IRepositories;
using SocialNetwork.Core.Application.ViewModels.User;
using SocialNetwork.Core.Domain.Entities;
using SocialNetwork.Infrastructure.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SocialNetwork.Infrastructure.Persistence.Repositories
{
        public class UserRepository : GenericRepository<Usuario>, IUserRepository
        {
            public readonly ApplicationContext applicationcon;
            public readonly IMapper mapper;


        public UserRepository(IMapper mapper, ApplicationContext context) : base(context)
            {
                applicationcon = context;
                 this.mapper = mapper;
            }

            public override async Task<Usuario> AddAsync(Usuario entity)
            {
                entity.Contraseña = PasswordEncrypter.PassHasher(entity.Contraseña);
                await base.AddAsync(entity);

                return entity;
            }


        public async Task<Usuario> LoginAsync(LoginViewModel logvm)
            {
                string passEncripted = PasswordEncrypter.PassHasher(logvm.Password);
            
                Usuario usuario = await applicationcon.Set<Usuario>().
                 FirstOrDefaultAsync(user => user.UserName == logvm.UserName && user.Contraseña == passEncripted);


                return usuario;
            }

            public async Task<bool> ValidateifExists(SaveUserViewModel uservm)
            {

                //string passEncripted = PasswordEncrypter.PassHasher(uservm.Contraseña);
                Usuario usuario = await applicationcon.Set<Usuario>().
                 FirstOrDefaultAsync(user => user.UserName == uservm.UserName /*|| user.Contraseña == passEncripted*/);


                return usuario == null ? false : true;
            }

        public async Task<UserViewModel> GetUserByNameAsync(string username)
        {
            Usuario usuario = await applicationcon.Set<Usuario>().
             FirstOrDefaultAsync(user => user.UserName == username);

            UserViewModel user = mapper.Map<UserViewModel>(usuario);

            return user;

        }


    
    }
    
}
