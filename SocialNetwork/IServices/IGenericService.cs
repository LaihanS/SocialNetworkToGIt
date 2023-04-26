namespace SocialNetwork.Core.Application.IServices
{
    public interface IGenericService<ViewModel, SaveViewModel, Entity>
         where Entity : class
        where ViewModel : class
         where SaveViewModel : class
    {
        Task<SaveViewModel> AddAsync(SaveViewModel vm);
        Task Delete(SaveViewModel vm,int id);
        Task EditAsync(SaveViewModel vm, int id);
        Task<List<ViewModel>> GetAsync();
        Task<SaveViewModel> GetEditAsync(int id);
    }
}