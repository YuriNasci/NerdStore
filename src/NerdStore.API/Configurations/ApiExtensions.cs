using System.Reflection;

namespace NerdStore.API.Configurations
{
    public static class ApiExtensions
    {
        public static void AddApiConfiguration(this IServiceCollection services, WebApplicationBuilder builder)
        {
            services.AddControllers();

            services.AddCors(options =>
            {
                options.AddPolicy("Total",
                    builder =>
                        builder
                            .AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader());
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            builder.Configuration
                   .SetBasePath(builder.Environment.ContentRootPath)
                   .AddJsonFile("appsettings.json", true, true)
                   .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
                   .AddEnvironmentVariables();

            if (builder.Environment.IsProduction())
            {
                builder.Configuration.AddUserSecrets(Assembly.GetExecutingAssembly(), true);
            }
        }
    }
}
