using BikeDoctor.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models; 
using BikeDoctor.Repository;
using BikeDoctor.Service;
using BankDB.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios al contenedor
builder.Services.AddControllers();

// Configurar el DbContext con la cadena de conexión desde appsettings.json
builder.Services.AddDbContext<BikeDoctorContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
           .EnableSensitiveDataLogging()
           .LogTo(Console.WriteLine, LogLevel.Information)); 

// Configuración de Repositories y Services
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IClientService, ClientService>();

// Agregar Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo 
    { 
        Title = "BikeDoctor API", 
        Version = "v1",
        Description = "API para la gestión de BikeDoctor",
        Contact = new OpenApiContact
        {
            Name = "Tu Nombre",
            Email = "tuemail@ejemplo.com"
        }
    });
});

var app = builder.Build();

// Configurar el pipeline de la aplicación
app.UseHttpsRedirection();

// Agregar middleware de Swagger
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "BikeDoctor API V1");
    c.RoutePrefix = string.Empty; // Hace que Swagger esté disponible en la raíz (/)
});

app.UseAuthorization();
app.MapControllers();

app.Run();