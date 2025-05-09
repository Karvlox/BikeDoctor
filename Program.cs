using BikeDoctor.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models; 
using BikeDoctor.Repository;
using BikeDoctor.Service;
using BikeDoctor.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios al contenedor
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });

// Configurar el DbContext con la cadena de conexión desde appsettings.json
builder.Services.AddDbContext<BikeDoctorContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
           .EnableSensitiveDataLogging()
           .LogTo(Console.WriteLine, LogLevel.Information)); 

// Configuración de Repositories y Services de Clientes y Motocicletas
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IMotorcycleRepository, MotorcycleRepository>();
builder.Services.AddScoped<IMotorcycleService, MotorcycleService>();

// Configuracion de Respositories y services del board de seguimiento
builder.Services.AddScoped<IReceptionRepository, ReceptionRepository>();
builder.Services.AddScoped<IReceptionService, ReceptionService>();
builder.Services.AddScoped<IDiagnosisRepository, DiagnosisRepository>();
builder.Services.AddScoped<IDiagnosisService, DiagnosisService>();
builder.Services.AddScoped<ISparePartsRepository, SparePartsRepository>();
builder.Services.AddScoped<ISparePartsService, SparePartsService>();
builder.Services.AddScoped<ICostApprovalRepository, CostApprovalRepository>();
builder.Services.AddScoped<ICostApprovalService, CostApprovalService>();
builder.Services.AddScoped<IRepairRepository, RepairRepository>();
builder.Services.AddScoped<IRepairService, RepairService>();
builder.Services.AddScoped<IQualityControlRepository, QualityControlRepository>();
builder.Services.AddScoped<IQualityControlService, QualityControlService>();
builder.Services.AddScoped<IDeliveryRepository, DeliveryRepository>();
builder.Services.AddScoped<IDeliveryService, DeliveryService>();

// Configuración de Repositories y Services del Tablero general del board
builder.Services.AddScoped<IFlowAttentionRepository, FlowAttentionRepository>();
builder.Services.AddScoped<IFlowAttentionService, FlowAttentionService>();

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