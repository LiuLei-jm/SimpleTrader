using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace SimpleTrader.EntityFramework;

public class SimpleTraderDbContextFactory
{
    private readonly string _connenctionString;

    public SimpleTraderDbContextFactory(string connenctionString)
    {
        _connenctionString = connenctionString;
    }

    public SimpleTraderDbContext CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<SimpleTraderDbContext>();

        options.UseSqlServer(_connenctionString);
        return new SimpleTraderDbContext(options.Options);
    }
}
