using ChustaSoft.Templates.WebApi.Tests.Configuration;

namespace ChustaSoft.Templates.WebApi.Tests.Base;

public abstract class IntegrationTestBase : IClassFixture<InternalWebApplicationFactory<Program>>
{

    protected readonly InternalWebApplicationFactory<Program> _factory;


    protected HttpClient Client => _factory.CreateClient();


    protected IntegrationTestBase(InternalWebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

}
