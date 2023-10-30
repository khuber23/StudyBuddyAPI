using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ApiStudyBuddy.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text.Json.Serialization;
using Microsoft.OpenApi.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ApiStudyBuddy.Endpoints;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ApiStudyBuddyContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ApiStudyBuddyContext") ?? throw new InvalidOperationException("Connection string 'ApiStudyBuddyContext' not found.")));

builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(opt =>
{
    opt.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
};

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.MapDeckEndpoints();

app.MapDeckFlashCardEndpoints();

app.MapDeckGroupEndpoints();

app.MapDeckGroupDeckEndpoints();

app.MapFlashCardEndpoints();

app.MapStudySessionEndpoints();

app.MapStudySessionFlashCardEndpoints();

app.MapUserEndpoints();

app.MapUserDeckEndpoints();

app.MapUserDeckGroupEndpoints();

app.Run();
