using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;


namespace ChustaSoft.Templates.WebApi.Tests.Configuration;

public class InternalWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            //Override the default configured services defining custom mocks within DI container here

        });

        builder.UseEnvironment("Development");
    }

}
