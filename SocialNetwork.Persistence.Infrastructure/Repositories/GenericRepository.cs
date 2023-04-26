﻿using Microsoft.EntityFrameworkCore;
using SocialNetwork.Core.Application.IRepositories;
using SocialNetwork.Infrastructure.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Infrastructure.Persistence.Repositories
{

    public class GenericRepository<Entity> : IGenericRepository<Entity> where Entity : class
    {
        private readonly ApplicationContext context;

        public GenericRepository(ApplicationContext context)
        {

            this.context = context;
        }

        public virtual async Task<Entity> AddAsync(Entity entity)
        {
            await context.Set<Entity>().AddAsync(entity);
            await context.SaveChangesAsync();

            return entity;
        }

        public virtual async Task EditAsync(Entity entity, int id)
        {
            Entity entry = await context.Set<Entity>().FindAsync(id);
            context.Entry(entry).CurrentValues.SetValues(entity);
            await context.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(Entity entity)
        {
            context.Set<Entity>().Remove(entity);
            await context.SaveChangesAsync();
        }

        public virtual async Task<List<Entity>> GetAsync()
        {
            return await context.Set<Entity>().ToListAsync();
        }
        public virtual async Task<List<Entity>> GetAsyncWithJoin(List<string> navProperties)
        {
            var query = context.Set<Entity>().AsQueryable();

            foreach (string item in navProperties)
            {
                query = query.Include(item);
            }

            return await query.ToListAsync();
        }

        public virtual async Task<Entity> GetByidAsync(int id)
        {
            return await context.Set<Entity>().FindAsync(id);
        }
    }
}
