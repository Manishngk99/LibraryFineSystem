﻿using FineTable.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FineTable.Infrastructure.Repository
{
    public class DatabaseContext : DbContext

    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
        }

        public DatabaseContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseNpgsql(connectionString);

                //#region remove the line below when going to production enviroment
                //optionsBuilder.EnableSensitiveDataLogging();
                //#endregion
            }
        }
        public DbSet<EFine> FineTable { get; set; }
        public DbSet<EFineCollection> FineCollection { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Call the base OnModelCreating to ensure default behavior is applied.
            base.OnModelCreating(modelBuilder);

            // Seed initial data for EFine table
            var initialData = SeedData.GetInitialEFineData();
            modelBuilder.Entity<EFine>().HasData(initialData);
        }
    }
}
