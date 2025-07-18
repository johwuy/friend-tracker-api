using friend_tracker_api.Model;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

const string dbName = "ApplicationDatabase";
var connectionString = builder.Configuration.GetConnectionString(dbName) ?? throw new InvalidOperationException($"Connection string '{dbName}' not found");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
