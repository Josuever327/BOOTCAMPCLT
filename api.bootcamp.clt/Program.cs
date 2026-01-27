using Api.BootCamp.Aplication.Interfaces;
using Api.BootCamp.Aplication.Query.GetProductByHandler;
using Api.BootCamp.Infrastructura.Context;
using Api.BootCamp.Infrastructura.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<PostegresDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("ProductosDb")));

builder.Services.AddScoped<IProductoRepository, ProductoRepository>();

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(GetProductoByIdHandler).Assembly));

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Bootcamp CLT API",
        Version = "v1",
        Description = "API REST para gestión de productos y categorías"
    });

    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Bootcamp CLT API v1 KIARA TORALES ");
    });
}
app.UseMiddleware<Api.BootCamp.Api.Middleware.ExceptionMiddleware>();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();