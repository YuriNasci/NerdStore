using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using NerdStore.Catalogo.Data.Constants;
using System;

namespace NerdStore.Catalogo.Data.Factories
{
    //public class CatalogoDatabaseContextFactory : IDesignTimeDbContextFactory<CatalogoContext>
    //{
    //    public CatalogoContext CreateDbContext(string[] args)
    //    {
    //        var dbContext = new CatalogoContext(new DbContextOptionsBuilder<CatalogoContext>().UseSqlServer(
    //             new ConfigurationBuilder()
    //                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
    //                    .AddJsonFile("appsettings.json", true, true)
    //                    .AddJsonFile($"appsettings.{EnvironmentName.Development}.json", true, true)
    //                    .AddJsonFile($"appsettings.{EnvironmentName.Production}.json", true, true)
    //                    .AddEnvironmentVariables()
    //                    .Build()
    //                    .GetConnectionString(ContextConstants.DB_CONNECTION_NAME)
    //             ).Options);
    //        dbContext.Database.Migrate();
    //        return dbContext;

    //    }

    //}
}
