using log4net;
using Microsoft.Extensions.Options;

namespace NETCore3WebApp.Infrastructure.DB
{
    public class DbContextGenerator : IDbContextGenerator
    {
        private readonly IOptions<Domain.AppSettings> options;
        private static readonly ILog Log = LogManager.GetLogger(typeof(DbContextGenerator));
        public DbContextGenerator(IOptions<Domain.AppSettings> options)
        {
            this.options = options;
        }

        public IMyDbContext GenerateMyDbContext()
        {
            Log.Debug("My Db Context Created with Connection String: " + options.Value.ConnString);
            return new MyDbContext(options.Value.ConnString);
        }

    }
}
