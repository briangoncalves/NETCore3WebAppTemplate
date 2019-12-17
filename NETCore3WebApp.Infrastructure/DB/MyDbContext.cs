using log4net;
using Microsoft.EntityFrameworkCore;

namespace NETCore3WebApp.Infrastructure.DB
{
    public class MyDbContext : DbContext, IMyDbContext

    {

        private readonly string _connectionString;

        private static readonly ILog Log = LogManager.GetLogger(typeof(MyDbContext));

        public DbSet<Domain.Entity> Entities { get; set; }

        public MyDbContext(string connectionString) : base()
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                Log.Debug("Context configured with connection string: " + _connectionString);
                optionsBuilder.UseSqlServer(_connectionString);
            }
        }

        public override int SaveChanges()
        {
            Log.Debug("Save Changes");
            return base.SaveChanges();

        }
    }
}
