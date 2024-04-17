using Api.Model.Migrations.MigScenario;
using Api.Model.ResponseModel.MicroController;
using Api.Model.ResponseModel.Music;
using Api.Model.ResponseModel.PlayList;
using Api.Model.ResponseModel.Scenario;
using Api.Model.ResponseModel.User;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.ApplicationContext;

public class ApplicationContext(IConfiguration configuration) : DbContext
{
    public DbSet<DtoPlayList> PlayLists => Set<DtoPlayList>();
    public DbSet<DtoMusic> Musics => Set<DtoMusic>();
    public DbSet<DtoUser> Users => Set<DtoUser>();
    public DbSet<MigScenario> Scenario => Set<MigScenario>();
    public DbSet<DtoMicroController> MicroControllers => Set<DtoMicroController>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySQL(configuration.GetConnectionString("MySql") ?? string.Empty);
    }
}