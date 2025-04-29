using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace SimpleTrader.EntityFramework;

public class SimpleTraderDbContextFactory
{
    private readonly Action<DbContextOptionsBuilder> _configureDbContext;

    public SimpleTraderDbContextFactory(Action<DbContextOptionsBuilder> configureDbContext)
    {
        this._configureDbContext = configureDbContext;
    }

    public SimpleTraderDbContext CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<SimpleTraderDbContext>();
        _configureDbContext(options);
        return new SimpleTraderDbContext(options.Options);
    }
}
