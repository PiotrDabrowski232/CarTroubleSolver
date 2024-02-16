using CarTroubleSolver.Data.Data;
using CarTroubleSolver.Data.Models;
using CarTroubleSolver.Data.Repositories;
using CarTroubleSolver.Data.Repositories.Interfaces;
using CarTroubleSolver.Logic.Services;
using CarTroubleSolver.Logic.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//DbContext
var connectionString = builder.Configuration.GetConnectionString("Connection");
builder.Services.AddDbContext<CarTroubleSolverDbContext>(options => options.UseSqlServer(connectionString), ServiceLifetime.Transient);


//DI
//Services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//Repos
builder.Services.AddScoped<IUserRepository, UserRepository>();

var app = builder.Build();

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
