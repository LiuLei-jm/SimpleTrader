using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services;
using SimpleTrader.EntityFramework.Services.Common;

namespace SimpleTrader.EntityFramework.Services
{
    public class AccountDataService : IDataService<Account>
    {
        private readonly SimpleTraderDbContextFactory _contextFactory;
        private readonly NonQueryDataService<Account> _nonQueryDataService;

        public AccountDataService(SimpleTraderDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
            _nonQueryDataService = new NonQueryDataService<Account>(contextFactory);
        }

        public async Task<Account> Create(Account entity)
        {
            return await _nonQueryDataService.CreateAsync(entity);
        }

        public async Task<bool> Delete(int id)
        {
            return await _nonQueryDataService.DeleteAsync(id);
        }

        public async Task<Account> Get(int id)
        {
            using (SimpleTraderDbContext context = _contextFactory.CreateDbContext())
            {
                var entity = await context
                    .Accounts.Include(a => a.AssetTransactions)
                    .FirstOrDefaultAsync(entity => entity.Id == id);
                return entity;
            }
        }

        public async Task<IEnumerable<Account>> GetAll()
        {
            using (SimpleTraderDbContext context = _contextFactory.CreateDbContext())
            {
                var entities = await context
                    .Accounts.Include(a => a.AssetTransactions)
                    .ToListAsync();
                return entities.AsEnumerable();
            }
        }

        public async Task<Account> Update(int id, Account entity)
        {
            using (SimpleTraderDbContext context = _contextFactory.CreateDbContext())
            {
                var existingEntity = await context
                    .Set<Account>()
                    .Include(a => a.AssetTransactions)
                    .FirstOrDefaultAsync(e => e.Id == id);
                if (existingEntity == null)
                {
                    return null;
                }
                entity.Id = existingEntity.Id;
                context.Entry(existingEntity).CurrentValues.SetValues(entity);
                existingEntity.AssetTransactions.Clear();
                foreach (var transaction in entity.AssetTransactions)
                {
                    existingEntity.AssetTransactions.Add(transaction);
                }
                await context.SaveChangesAsync();
                return existingEntity;
            }
        }
    }
}
