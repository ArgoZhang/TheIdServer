﻿// Project: Aguafrommars/TheIdServer
// Copyright (c) 2020 @Olivier Lefebvre
using Aguacongas.TheIdServer.Data;
using Aguacongas.TheIdServer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Aguacongas.TheIdServer.MySql
{
    [SuppressMessage("Major Code Smell", "S1118:Utility classes should not have public constructors", Justification = "<Pending>")]
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(builder => builder.AddJsonFile("appsettings.json"))
                .ConfigureServices((hostContext, services) =>
                {
                    var cn = hostContext.Configuration.GetConnectionString("db");

                    Action<DbContextOptionsBuilder> optionsAction = options => options.UseMySql(cn, options => options.MigrationsAssembly("Aguacongas.TheIdServer.Migrations.MySql"));
                    services.AddDbContext<ApplicationDbContext>(optionsAction)
                        .AddIdentityServer4AdminEntityFrameworkStores<ApplicationUser, ApplicationDbContext>()
                        .AddConfigurationEntityFrameworkStores(optionsAction)
                        .AddOperationalEntityFrameworkStores(optionsAction);
                });

    }
}
