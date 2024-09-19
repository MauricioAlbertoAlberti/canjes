using Canjes.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Agregar el Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//agregar el contexto ESTO es Inyeccion de Dependencias
builder.Services.AddDbContext<CanjeContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("miConeccion")));

// configurar el json para ignorar cycles de respuesta
builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

//  cors Configurar
builder.Services.AddCors(o => o.AddPolicy("misCors", builder =>
{
    builder.AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
}));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//uso de Cors
app.UseCors("misCors");

app.UseAuthorization();

app.MapControllers();

app.Run();
