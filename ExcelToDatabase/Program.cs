using ExcelToDatabase.Data;
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
builder.Services.AddScoped<IBDservices, BDservices>();
builder.Services.AddScoped<IExcelInterface, ExcelServices>();
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
