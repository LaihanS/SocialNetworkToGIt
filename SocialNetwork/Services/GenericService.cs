using AutoMapper;
using SocialNetwork.Core.Application.IRepositories;
using SocialNetwork.Core.Application.IServices;
using SocialNetwork.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Application.Services
{
    public class GenericService<ViewModel, SaveViewModel, Entity> : IGenericService<ViewModel, SaveViewModel, Entity>
        where Entity : class
        where ViewModel : class
         where SaveViewModel : class
    {
        private readonly IGenericRepository<Entity> repository;
        private readonly IMapper automapper;

        public GenericService(IMapper automapper, IGenericRepository<Entity> repository)
        {

            this.repository = repository;
            this.automapper = automapper;
        }


        public virtual async Task<SaveViewModel> AddAsync(SaveViewModel vm)
        {
            Entity usuario = automapper.Map<Entity>(vm);

            usuario = await repository.AddAsync(usuario);

            SaveViewModel saveUser = automapper.Map<SaveViewModel>(usuario);

            return saveUser;
        }


        public virtual async Task<List<ViewModel>> GetAsync()
        {
            var usuarioslist = await repository.GetAsync();

            List<ViewModel> listvm = automapper.Map<List<ViewModel>>(usuarioslist);

            return listvm;

        }

        public virtual async Task<SaveViewModel> GetEditAsync(int id)
        {

            var usuario = await repository.GetByidAsync(id);

            SaveViewModel saveUser = automapper.Map<SaveViewModel>(usuario);

            return saveUser;

        }

        public virtual async Task Delete(SaveViewModel vm, int id)
        {
            Entity usuario = await repository.GetByidAsync(id);
            await repository.DeleteAsync(usuario);
        }

        public virtual async Task EditAsync(SaveViewModel vm, int id)
        {
            Entity usuario = await repository.GetByidAsync(id);
            //string encryptedPassword = PasswordEncrypter.PassHasher(vm.Contraseña);

            usuario = automapper.Map<Entity>(vm);

            await repository.EditAsync(usuario, id);
        }
    }
}
