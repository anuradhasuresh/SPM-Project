using Microsoft.AspNetCore.Mvc;
using CalorieCounterAPI;
using CalorieCounterAPI.Controllers;
using CalorieCounterAPI.Repositories;
using CalorieCounterAPI.Models;
using CalorieCounterAPI.Interfaces;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CalorieCounterAPI.Data
{
    /// <summary>
    /// provides MySQL DB connection and context
    /// </summary>
    public class DataContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public DataContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to mysql with connection string from app settings
            var connectionString = Configuration.GetConnectionString("default");
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }
        //public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<CalorieClass> Calorie { get; set; }
    }
}