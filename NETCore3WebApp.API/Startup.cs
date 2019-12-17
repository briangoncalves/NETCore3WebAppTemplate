using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using NETCore3WebApp.API.Logger;

namespace NETCore3WebApp.API
{
    public class Startup
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(Startup));
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            log.Info("Application Started");
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureCompression(services);
            log.Info("Compression Configured");
            ConfigureInjectDependencies(services);
            log.Info("Dependency Injection Configured");
            ConfigureCors(services);
            log.Info("Cors Configured");
            services.AddControllers();
            ConfigureSwagger(services);
            log.Info("Swagger Configured");
            services.AddMvc().SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_3_0)
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver =
                    new DefaultContractResolver();
            });
        }

        private static void ConfigureCompression(IServiceCollection services)
        {
            services.AddResponseCompression();
            services.AddResponseCompression(options => options.Providers.Add<GzipCompressionProvider>());
        }

        private void ConfigureInjectDependencies(IServiceCollection services)
        {
            MyInjectionMiddleware.Config(services: services);
        }

        private void ConfigureSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1.0.0.0", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "My API Documentation",
                    Version = "v1.0.0.0",
                    Description = "My API"
                });
            });
        }

        private void ConfigureCors(IServiceCollection services)
        {
            services.AddCors(o => o.AddPolicy("Cors", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader()
                       .WithExposedHeaders("Content-Disposition", "Content-Length");
            }));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddLog4Net();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseExceptionHandler(new ExceptionHandlerOptions
            {
                ExceptionHandler = new JsonExceptionMiddleware(env).Invoke
            });

            app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("../swagger/v1.0.0.0/swagger.json", "My API Documentation v1.0.0.0");
            });
        }
    }
}
