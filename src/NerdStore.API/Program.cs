using Microsoft.AspNetCore.Localization;
using System.Globalization;
using NerdStore.API.Extensions;
using NerdStore.Catalogo.Data.Extensions;
using NerdStore.API.Configurations;
using NerdStore.Vendas.Data.Extensions;
using NerdStore.Pagamentos.Data.Extensions;


var builder = WebApplication.CreateBuilder(args);

// Add builder.Services to the container.

builder.Services.AddApiConfiguration(builder);
builder.Services.AddCatalogoData(builder.Configuration);
builder.Services.AddVendasData(builder.Configuration);
builder.Services.AddPagamentosData(builder.Configuration);

builder.Services.RegisterServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.SeedCatalogoData(app.Configuration);
    app.SeedVendasFromScripts(app.Configuration);
    app.SeedPagamentosFromScripts(app.Configuration);
}

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
