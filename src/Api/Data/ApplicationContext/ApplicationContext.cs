﻿using Api.Model.Migration.MicroController;
using Api.Model.Migration.Music;
using Api.Model.Migration.PlayList;
using Api.Model.Migration.Scenario;
using Api.Model.Migration.User;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.ApplicationContext;

public class ApplicationContext(IConfiguration configuration) : DbContext
{
    public DbSet<MigrationPlayList> PlayLists => Set<MigrationPlayList>();
    public DbSet<MigrationMusic> Musics => Set<MigrationMusic>();
    public DbSet<MigrationUser> Users => Set<MigrationUser>();
    public DbSet<MigrationScenario> Scenario => Set<MigrationScenario>();
    public DbSet<MigrationMicroController> MicroControllers => Set<MigrationMicroController>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySQL(configuration.GetConnectionString("MySql"));
    }
}