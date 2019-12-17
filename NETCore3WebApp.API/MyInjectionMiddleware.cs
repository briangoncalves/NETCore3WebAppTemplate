using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NETCore3WebApp.Business;
using NETCore3WebApp.Infrastructure.DB;
using System.IO;

namespace NETCore3WebApp.API
{
    public static class MyInjectionMiddleware
    {
        /// <summary>
        ///   Metodo de entrada para configuração de todas injeções de dependencias 
        ///   do ambiente trade finance
        /// </summary>
        /// <param name="services">objeto de serviço responsavel por prover metodos de configuração</param>
        public static void Config(IServiceCollection services)
        {
            services.AddOptions();
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            var configuration = builder.Build();
            services.Configure<Domain.AppSettings>(configuration.GetSection("MyDb"));
            configureDbContext(services);
            configureBusinessManagers(services);
        }

        private static void configureDbContext(IServiceCollection services)
        {
            services.AddTransient<IDbContextGenerator, DbContextGenerator>();
        }

        private static void configureBusinessManagers(IServiceCollection services)
        {
            services.AddTransient<IMyEntityManager, MyEntityManager>();
        }
    }
}
