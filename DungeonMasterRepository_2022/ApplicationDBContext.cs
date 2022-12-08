﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using DungeonMasterDTO_2022;

namespace DungeonMasterRepository_2022
{
    public class ApplicationDBContext : DbContext
    {
        public DbSet<Item> Items { get; set; }

        public ApplicationDBContext()
        {

        }

        public ApplicationDBContext(DbContextOptions options) : base(options)
        {

        }

        private static IConfigurationRoot _configuration;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

                _configuration = builder.Build();
                string cnstr = _configuration.GetConnectionString("DungeonManager");
                optionsBuilder.UseSqlServer(cnstr);
            }
        }



    }
}