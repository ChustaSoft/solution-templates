using ChustaSoft.Templates.WebApi.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services
//#if (apiDoc == "swagger")
    .AddVersioning()
    .AddSwagger();
//#endif
//#if (apiDoc == "scalar")
    .AddVersioning();
//#endif

var app = builder.Build();

app.UseHttpsRedirection();

app.MapEndpoints();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
//#if (apiDoc == "swagger")
    app.ConfigureSwagger();
//#endif
//#if (apiDoc == "scalar")
    app.MapScalarApiReference();
//#endif
}
app.Run();