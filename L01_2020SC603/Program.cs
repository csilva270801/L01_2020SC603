using Microsoft.EntityFrameworkCore;
using L01_2020SC603.Models;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Add services to the container.

builder.Services.AddControllers();

//Inyecc��n por dependencia de conexion al contexto
builder.Services.AddDbContext<restauranteContext>(options =>
         options.UseSqlServer(
                 builder.Configuration.GetConnectionString("restauranteDbconnection"))
         );

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
