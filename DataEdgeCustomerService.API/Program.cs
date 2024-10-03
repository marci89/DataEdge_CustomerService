using DataEdge_CustomerService.Business.Services;
using DataEdge_CustomerService.Business.Services.Interfaces;
using DataEdge_CustomerService.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<DataBaseImportService >();
builder.Services.AddScoped<ShopService>();
builder.Services.AddScoped<ItemService>();
builder.Services.AddScoped<PurchaseService>();
builder.Services.AddScoped<PurchaseItemService>();

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseNpgsql("Host=localhost;Port=5432;Database=DataEdge;Username=postgres;Password=Password;");
});

var version = Assembly.GetExecutingAssembly().GetName().Version;

builder.Services.AddSwaggerGen(c =>
    {
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Dataedge customer Service",
        Version = $"v{version}",
        Description = "Dataedge customer Service",
        Contact = new OpenApiContact
        {
            Email = "kismarczirobi@gmail.com",
            Name = "József Kismarczi"
        }
    });
    });


    builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

#region If database is not exists or there is a new migration to update, this will handle it.


    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<DataContext>();
    if (db.Database.GetPendingMigrations().Any())
    {
        db.Database.Migrate();
    }

#endregion

app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().WithOrigins("*"));

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
