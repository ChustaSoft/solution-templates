using ChustaSoft.Templates.WebApi.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddVersioning()
    .AddSwagger();


var app = builder.Build();

app.UseHttpsRedirection();

app.MapEndpoints();

if (app.Environment.IsDevelopment())
    app.ConfigureSwagger();

app.Run();