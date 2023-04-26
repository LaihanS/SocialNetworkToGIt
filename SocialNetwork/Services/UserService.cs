using AutoMapper;
using SocialNetwork.Core.Application.Helpers;
using SocialNetwork.Core.Application.IRepositories;
using SocialNetwork.Core.Application.IServices;
using SocialNetwork.Core.Application.ViewModels.User;
using SocialNetwork.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Application.Services
{
    public class UserService : GenericService<UserViewModel, SaveUserViewModel, Usuario>, IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper imapper;
        public UserService(IUserRepository userRepository, IMapper imapper) : base(imapper, userRepository)
        {
            this.userRepository = userRepository;
            this.imapper = imapper;
        }

        public async Task<UserViewModel> LoginAsync(LoginViewModel vm)
        {

            Usuario usuario = await userRepository.LoginAsync(vm);

            if (usuario == null)
            {
                return null;
            }

            UserViewModel saveUser = imapper.Map<UserViewModel>(usuario);

            return saveUser;
        }

        public async Task<bool> Exists(SaveUserViewModel vm)
        {
            bool exists = await userRepository.ValidateifExists(vm);

            if (exists)
            {
                return true;
            }

            return false;
        }


        public virtual async Task EditEncryptAsync(SaveUserViewModel vm, int id)
        {
            Usuario usuario = await userRepository.GetByidAsync(id);

            usuario = imapper.Map<Usuario>(vm);
            usuario.Contraseña = PasswordEncrypter.PassHasher(vm.Contraseña);

            await userRepository.EditAsync(usuario, id);
        }


        public async Task<UserViewModel> ValidateNameAsync(string username)
        {
            UserViewModel user = await userRepository.GetUserByNameAsync(username);

            return user;
        }

    }
}
