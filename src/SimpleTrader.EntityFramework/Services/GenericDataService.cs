using Microsoft.EntityFrameworkCore;
using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services;
using SimpleTrader.EntityFramework.Services.Common;

namespace SimpleTrader.EntityFramework.Services
{
    public class GenericDataService<T> : IDataService<T>
        where T : DomainObject
    {
        private readonly SimpleTraderDbContextFactory _contextFactory;
        private readonly NonQueryDataService<T> _nonQueryDataService;

        public GenericDataService(SimpleTraderDbContextFactory contextFactory, NonQueryDataService<T> nonQueryDataService)
        {
            _contextFactory = contextFactory;
            _nonQueryDataService = nonQueryDataService;
        }

        public async Task<T> Create(T entity)
        {
            return await _nonQueryDataService.CreateAsync(entity);
        }

        public async Task<bool> Delete(int id)
        {
            return await _nonQueryDataService.DeleteAsync(id);
        }

        public async Task<T?> Get(int id)
        {
            using (SimpleTraderDbContext context = _contextFactory.CreateDbContext())
            {
                var entity = await context.Set<T>().FirstOrDefaultAsync(entity => entity.Id == id);
                if (entity == null)
                {
                    return null;
                }
                return entity;
            }
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            using (SimpleTraderDbContext context = _contextFactory.CreateDbContext())
            {
                var entities = await context.Set<T>().ToListAsync();
                return entities;
            }
        }

        public async Task<T?> Update(int id, T entity)
        {
            return await _nonQueryDataService.UpdateAsync(id, entity);
        }
    }
}
