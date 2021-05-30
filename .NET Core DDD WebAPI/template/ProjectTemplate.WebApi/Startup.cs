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

            // Registration of services needed for the supporting ddd infrastructure (such as the bus for processing events)
            services.RegisterDddFrameworkService();

            // Registration of services needed for the application layer, where Dto's are translated to commands
            // and send into the central processing system (bus)
            services.ConfigureApplicationServices();

            // Registration of services needed for retrieving read models
            services.ConfigureApplicationReadServices();

            // Registration of implementation of the eventstore. This example uses a table in an T-SQL database to store the events
            services.AddSingleton<IEventStore>(x => new TSqlEventStore(GetConnectionString()));

            // Registration of a service that provides an implementation for retrieval of the read model. The same database is used as for the event store.
            // The data is written and updated using entity framework, and read with Dapper.
            // Here, speed is of the essence, so the information in the database is already prepared for consumption, and kept up-to-date using projections.
            services.AddSingleton<IReadModelRepository<UserProfileReadModel>>(x => new TSqlReadModelRepository<UserProfileReadModel>(GetConnectionString()));

            // Registration of domain services, such as command and event handlers, repositories, projections and policies
            services.ConfigureDomainServices();

            // Registration of services that provide the readmodel implementations
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

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }

        private string GetConnectionString()
        {
            return _configuration.GetConnectionString("DefaultDatabase");
        }
    }
}
