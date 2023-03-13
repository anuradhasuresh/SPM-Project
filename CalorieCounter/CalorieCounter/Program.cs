using Microsoft.EntityFrameworkCore;
using CalorieCounterAPI;
using CalorieCounterAPI.Data;
using CalorieCounterAPI.Controllers;
using CalorieCounterAPI.Repositories;
using CalorieCounterAPI.Models;
using CalorieCounterAPI.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddTransient<Seed>();
builder.Services.AddControllers().AddJsonOptions(x =>
x.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// add connection to MySQL database
builder.Services.AddEntityFrameworkMySql()
    .AddDbContext<DataContext>(options =>
        options.UseMySql(builder.Configuration.GetConnectionString("default"),
        new MySqlServerVersion(new Version()))
    );

//add repository references
builder.Services.AddScoped<ICalorieRepository, CalorieRepository>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

SeedData(app);

void SeedData(IHost app)
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();
    using (var scope = scopedFactory.CreateScope())
    {
        var service = scope.ServiceProvider.GetService<Seed>();
        service.SeedDataContext();
    }
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
