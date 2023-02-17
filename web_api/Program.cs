using Microsoft.EntityFrameworkCore;
using System.Reflection;

using web_api.Models;

var builder = WebApplication.CreateBuilder(args);

string dbFileName = "dbSqlite.db";

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DatabaseContext>(options =>
{
    options.UseSqlite(connectionString: "Filename=" + dbFileName,
            sqliteOptionsAction: op =>
            {
                op.MigrationsAssembly(
                    Assembly.GetExecutingAssembly().FullName
                );
            });
});

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
