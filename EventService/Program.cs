using EventService.Helpers;
using EventService.Models.Interfaceimplements;
using EventService.Models.Interfaces;
using EventService.Models.ViewModels.Mappers;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.что это
var services=builder.Services;
services.AddServices();

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
