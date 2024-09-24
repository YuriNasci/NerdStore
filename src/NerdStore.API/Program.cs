using Microsoft.AspNetCore.Localization;
using System.Globalization;
using NerdStore.API.Extensions;
using NerdStore.Catalogo.Data.Extensions;
using NerdStore.API.Configurations;
using NerdStore.Vendas.Data.Extensions;
using NerdStore.Pagamentos.Data.Extensions;
using NerdStore.Catalogo.Data.Storage;

var builder = WebApplication.CreateBuilder(args);

#region Services
builder.Services.AddApiConfiguration(builder);
builder.Services.AddCatalogoData(builder.Configuration);
builder.Services.AddVendasData(builder.Configuration);
builder.Services.AddPagamentosData(builder.Configuration);
builder.Services.AddScoped<AzureStorageAccount>();
#endregion

builder.Services.RegisterServices();

var app = builder.Build();

#region Swagger
app.UseSwagger();
app.UseSwaggerUI();
#endregion

app.SeedCatalogoData(app.Configuration);
app.SeedVendasFromScripts(app.Configuration);
app.SeedPagamentosFromScripts(app.Configuration);

var supportedCultures = new[] { new CultureInfo("pt-BR") };
app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("pt-BR"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures
});

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("Total");

app.UseAuthorization();

app.UseMiddleware<ExceptionMiddleware>();

app.MapControllers();

app.Run();
