using ProjectTemplate.Application;
using ProjectTemplate.Domain;
using ProjectTemplate.Framework;
using ProjectTemplate.Framework.Repositories;
using ProjectTemplate.Infrastructure;
using ProjectTemplate.Infrastructure.DataStore.EventStore;
using ProjectTemplate.Infrastructure.DataStore.ReadModelStore;
using ProjectTemplate.Query.Application;
using ProjectTemplate.Query.Infrastructure;
using ProjectTemplate.Query.Dto.Users;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ProjectTemplate.Framework.Events;

namespace ProjectTemplate.WebApi
{
    public class Startup
    {
        public readonly IConfiguration _configuration;


        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ProjectTemplate.WebApi", Version = "v1" });
            });

            services.RegisterDddFrameworkService();
            services.ConfigureApplicationServices();
            services.ConfigureApplicationReadServices();
            services.AddSingleton<IEventStore>(x => new TSqlEventStore(GetConnectionString()));
            services.AddSingleton<IReadModelRepository<UserProfileReadModel>>(x => new TSqlReadModelRepository<UserProfileReadModel>(GetConnectionString()));
            services.ConfigureDomainServices();
            services.ConfigureReadModelServices(GetConnectionString());
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.InitializeSqlDatabase(GetConnectionString());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProjectTemplate.WebApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(p => p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }

        private string GetConnectionString()
        {
            return _configuration.GetConnectionString("DefaultDatabase");
        }

    }
}
