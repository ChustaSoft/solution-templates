using $ext_safeprojectname$.Application;
using $ext_safeprojectname$.Domain;
using $ext_safeprojectname$.Framework;
using $ext_safeprojectname$.Framework.Repositories;
using $ext_safeprojectname$.Infrastructure;
using $ext_safeprojectname$.Infrastructure.DataStore.EventStore;
using $ext_safeprojectname$.Infrastructure.DataStore.ReadModelStore;
using $ext_safeprojectname$.Query.Application;
using $ext_safeprojectname$.Query.Infrastructure;
using $ext_safeprojectname$.Query.Dto.Users;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using $ext_safeprojectname$.Framework.Events;

namespace $ext_safeprojectname$.WebApi
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
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "$ext_safeprojectname$.WebApi", Version = "v1" });
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
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "$ext_safeprojectname$.WebApi v1"));
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
