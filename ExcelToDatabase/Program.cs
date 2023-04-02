using ExcelToDatabase.Data;
using ExcelToDatabase.Facade;
using ExcelToDatabase.Facade.Interface;
using ExcelToDatabase.Models;
using ExcelToDatabase.Repository;
using ExcelToDatabase.Repository.Interfaces;
using ExcelToDatabase.Services;
using ExcelToDatabase.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var sqlConnect = builder.Configuration.GetSection("sqlConnection").Get<SqlConnect>();
builder.Services.AddDbContext<DBDataContext>(options => options.UseSqlServer(sqlConnect.ConnectionString));
builder.Services.AddScoped<IBDrepository, BDrepository>();
builder.Services.AddScoped<IExcelInterface, ExcelServices>();
builder.Services.AddScoped<IReadStream, ReadStreamService>();
builder.Services.AddScoped<IExcelFacade, ExcelFacade>();
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
