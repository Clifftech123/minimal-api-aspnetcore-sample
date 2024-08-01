using FluentValidation;
using Microsoft.EntityFrameworkCore;
using minimal_api_aspnetcore_sample.Infrastructure.Context;
using System.Reflection;

namespace minimal_api_aspnetcore_sample.Infrastructure.Extensions
{
    public static class ApplicationExtensions
    {

        public static void AddApplicationServices(this IHostApplicationBuilder builder)
        {
            builder.Services.AddDbContext<ApplicationContext>(configure =>
            {
                configure.UseSqlServer(builder.Configuration.GetConnectionString("sqlConnection"));
            });

            builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        }



    }
}

