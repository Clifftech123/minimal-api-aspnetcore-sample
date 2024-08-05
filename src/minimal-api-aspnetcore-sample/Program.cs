using minimal_api_aspnetcore_sample.Endpoints;
using minimal_api_aspnetcore_sample.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddApplicationServices();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
