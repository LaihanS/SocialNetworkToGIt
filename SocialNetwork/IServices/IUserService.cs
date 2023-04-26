using SocialNetwork.Core.Application.ViewModels.User;
using SocialNetwork.Core.Domain.Entities;

namespace SocialNetwork.Core.Application.IServices
{
    public interface IUserService: IGenericService<UserViewModel, SaveUserViewModel,Usuario> 
    {
        Task<bool> Exists(SaveUserViewModel vm);
        Task<UserViewModel> LoginAsync(LoginViewModel vm);
        Task<UserViewModel> ValidateNameAsync(string username);

        Task EditEncryptAsync(SaveUserViewModel vm, int id);
    }
}