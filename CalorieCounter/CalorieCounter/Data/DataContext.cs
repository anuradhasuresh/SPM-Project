using Microsoft.AspNetCore.Mvc;
using CalorieCounterAPI;
using CalorieCounterAPI.Controllers;
using CalorieCounterAPI.Repositories;
using CalorieCounterAPI.Models;
using CalorieCounterAPI.Interfaces;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;

namespace CalorieCounterAPI.Data
{
    /// <summary>
    /// provides MySQL DB connection and context
    /// </summary>
    public class DataContext : DbContext
    {
        // Get environment variable values for MySQL database user and password
        public static string User =>
        Environment.GetEnvironmentVariable("DB_USER");

        public static string Password =>
        Environment.GetEnvironmentVariable("DB_PASSWORD");
        protected readonly IConfiguration Configuration;

        public DataContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to mysql with connection string 
            // Console.WriteLine("Creds: " + User + "," + Password);
            var connectionString = "server=localhost;port=3306;user=" + User + ";password=" + Password + ";database=Calorie_DB;";
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }
        //public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<CalorieClass> Calorie { get; set; }
        public DbSet<Goal_IntakeClass> Goal_Intake { get; set; }
    }

}