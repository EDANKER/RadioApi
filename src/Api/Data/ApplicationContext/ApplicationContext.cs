﻿using Api.Model.Migrations.Scenario.PlayMigrationsScenario;
using Api.Model.Migrations.Scenario.TimeMigrationsScenario;
using Api.Model.ResponseModel.MicroController;
using Api.Model.ResponseModel.Music;
using Api.Model.ResponseModel.PlayList;
using Api.Model.ResponseModel.User;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.ApplicationContext;

public class ApplicationContext(IConfiguration configuration) : DbContext
{
    public DbSet<DtoPlayList> PlayLists => Set<DtoPlayList>();
    public DbSet<DtoMusic> Musics => Set<DtoMusic>();
    public DbSet<DtoUser> Users => Set<DtoUser>();
    public DbSet<TimeMigrationsScenario> TimeScenarios => Set<TimeMigrationsScenario>();
    public DbSet<PlayMigrationsScenario> PlayScenarios => Set<PlayMigrationsScenario>();
    public DbSet<DtoMicroController> MicroControllers => Set<DtoMicroController>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySQL(configuration.GetConnectionString("MySql") ?? string.Empty);
    }
}