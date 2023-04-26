﻿namespace SocialNetwork.Core.Application.IRepositories
{
    public interface IGenericRepository<Entity> where Entity : class
    {
        Task<Entity> AddAsync(Entity entity);
        Task DeleteAsync(Entity entity);
        Task EditAsync(Entity entity, int id);
        Task<List<Entity>> GetAsync();
        Task<List<Entity>> GetAsyncWithJoin(List<string> navProperties);
        Task<Entity> GetByidAsync(int id);
    }
}