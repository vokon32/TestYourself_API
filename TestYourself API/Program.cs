using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TestYourself_API.Data;
using TestYourself_API.Helper;
using TestYourself_API.Helpers;
using TestYourself_API.Models;
using TestYourself_API.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder = ConfigureBuilder.AddServices(builder);

var app = builder.Build();

//Seed.SeedData(app);
//await Seed.SeedUsersAndRolesAsync(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
