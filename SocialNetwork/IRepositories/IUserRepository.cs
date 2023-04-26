using SocialNetwork.Core.Application.ViewModels.User;
using SocialNetwork.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Application.IRepositories
{
    public interface IUserRepository: IGenericRepository<Usuario>
    {

        Task<bool> ValidateifExists(SaveUserViewModel uservm);
        Task<Usuario> LoginAsync(LoginViewModel logvm);
        Task<Usuario> AddAsync(Usuario entity);

        Task<UserViewModel> GetUserByNameAsync(string username);
    }
}
