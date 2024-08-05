using Microsoft.OpenApi.Models;
using minimal_api_aspnetcore_sample.Endpoints;
using minimal_api_aspnetcore_sample.Infrastructure.Extensions;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.AddApplicationServices();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c=>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Mimal API", Version = "v1", Description = "Showing how you can build minimal " +
        "api with .net" });


    // Set the comments path for the Swagger JSON and UI.
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);

}


    );

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mimal API");
        c.RoutePrefix = string.Empty;

    });
}

app.UseHttpsRedirection();
app.UseExceptionHandler();

app.MapGroup("/api/v1/")
   .WithTags("Instruction endpoints ")
   .MapInstructionEndpoints();

app.MapGroup("/api/v1/")
   .WithTags(" Ingredient endpoints ")
   .MapIngredientEndpoints();

app.MapGroup("/api/v1/")
    .WithTags(" Recipe endpoints ")
    .MapRecipeEndPoints();

app.Run();
