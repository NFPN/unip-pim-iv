using BlackRiver.EntityModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlackRiver.Data.Services
{
    public class GenericDataService<T> : IDataService<T> where T : BaseModel
    {
        public BlackRiverDBContextFactory ContextFactory { get; set; }

        public GenericDataService(BlackRiverDBContextFactory contextFactory)
            => this.ContextFactory = contextFactory;

        public async Task<T> Create(T entity)
        {
            using var context = ContextFactory.CreateDbContext();
            var result = await context.Set<T>().AddAsync(entity);
            await context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                using var context = ContextFactory.CreateDbContext();
                var entity = await context.Set<T>().FirstOrDefaultAsync((e) => e.Id == id);
                context.Set<T>().Remove(entity);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            using var context = ContextFactory.CreateDbContext();
            return await context.Set<T>().ToListAsync();
        }

        public async Task<T> Get(int id)
        {
            using var context = ContextFactory.CreateDbContext();
            return await context.Set<T>().FirstOrDefaultAsync((e) => e.Id == id);
        }

        public async Task<T> Update(int id, T entity)
        {
            using var context = ContextFactory.CreateDbContext();
            entity.Id = id;
            context.Set<T>().Update(entity);
            await context.SaveChangesAsync();
            return entity;
        }
    }
}