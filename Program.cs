using ActiveDirectory_API.Models;
using ActiveDirectory_API.Repositories;
using ActiveDirectory_API.Repositories.IRepositories;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped(typeof(IUserService), typeof(UserService));
var identitySettingsSection = builder.Configuration.GetSection("AppSettings");
builder.Services.Configure<AppSettings>(identitySettingsSection);

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
