using minimal_api_aspnetcore_sample.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddApplicationServices();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}

app.UseHttpsRedirection();


app.Run();