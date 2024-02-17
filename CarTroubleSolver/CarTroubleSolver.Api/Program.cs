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
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "CarTroubleSolver.Api", Version = "v1" });
});

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
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CarTroubleSolver.Api v1"));
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
