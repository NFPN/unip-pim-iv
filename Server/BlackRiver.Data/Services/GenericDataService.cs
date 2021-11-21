using BlackRiver.EntityModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlackRiver.Data.Services
{
    public class GenericDataService<T> : IDataService<T> where T : BaseModel
    {
        private readonly BlackRiverDBContextFactory contextFactory;

        public GenericDataService(BlackRiverDBContextFactory contextFactory)
            => this.contextFactory = contextFactory;

        public async Task<T> CreateData(T entity)
        {
            using var context = contextFactory.CreateDbContext();
            var result = await context.Set<T>().AddAsync(entity);
            await context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<bool> DeleteData(int id)
        {
            try
            {
                using var context = contextFactory.CreateDbContext();
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

        public async Task<IEnumerable<T>> GetAllData()
        {
            using var context = contextFactory.CreateDbContext();
            return await context.Set<T>().ToListAsync();
        }

        public async Task<T> GetData(int id)
        {
            using var context = contextFactory.CreateDbContext();
            return await context.Set<T>().FirstOrDefaultAsync((e) => e.Id == id);
        }

        public async Task<T> UpdateData(int id, T entity)
        {
            using var context = contextFactory.CreateDbContext();
            entity.Id = id;
            context.Set<T>().Update(entity);
            await context.SaveChangesAsync();
            return entity;
        }
    }
}