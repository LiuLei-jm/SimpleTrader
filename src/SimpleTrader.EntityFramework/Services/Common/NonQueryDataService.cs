using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SimpleTrader.Domain.Models;

namespace SimpleTrader.EntityFramework.Services.Common
{
    public class NonQueryDataService<T>
        where T : DomainObject
    {
        private readonly SimpleTraderDbContextFactory _contextFactory;

        public NonQueryDataService(SimpleTraderDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<T> CreateAsync(T entity)
        {
            using (SimpleTraderDbContext context = _contextFactory.CreateDbContext())
            {
                var createEntity = await context.Set<T>().AddAsync(entity);
                await context.SaveChangesAsync();

                return createEntity.Entity;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using (SimpleTraderDbContext context = _contextFactory.CreateDbContext())
            {
                var entity = await context.Set<T>().FirstOrDefaultAsync(entity => entity.Id == id);
                if (entity == null)
                {
                    return false;
                }
                context.Set<T>().Remove(entity);
                await context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<T> UpdateAsync(int id, T entity)
        {
            using (SimpleTraderDbContext context = _contextFactory.CreateDbContext())
            {
                var existingEntity = await context.Set<T>().FirstOrDefaultAsync(e => e.Id == id);
                if (existingEntity == null)
                {
                    return null;
                }
                entity.Id = existingEntity.Id;
                context.Entry(existingEntity).CurrentValues.SetValues(entity);
                await context.SaveChangesAsync();
                return entity;
            }
        }
    }
}
