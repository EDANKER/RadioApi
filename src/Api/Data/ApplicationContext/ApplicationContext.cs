using Api.Model.ResponseModel.User;
using Microsoft.EntityFrameworkCore;
using Radio.Model.Music;
using Radio.Model.PlayList;
using Radio.Model.ResponseModel.Scenari;

namespace Api.Data.ApplicationContext;

public class ApplicationContext(IConfiguration configuration) : DbContext
{
    public DbSet<GetPlayList> PlayLists => Set<GetPlayList>();
    public DbSet<GetMusic> Musics => Set<GetMusic>();
    public DbSet<GetUser> Users => Set<GetUser>();
    public DbSet<GetScenario> Scenario => Set<GetScenario>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySQL(configuration.GetConnectionString("MySql"));
    }
}