using Microsoft.EntityFrameworkCore;
using Radio.Model.Music;
using Radio.Model.PlayList;
using Radio.Model.ResponseModel.Scenari;
using Radio.Model.ResponseModel.User;

namespace Radio.Data.ApplicationContext;

public class ApplicationContext : DbContext
{
    public DbSet<GetPlayList> PlayLists => Set<GetPlayList>();
    public DbSet<GetMusic> Musics => Set<GetMusic>();
    public DbSet<GetUser> Users => Set<GetUser>();
    public DbSet<GetScenari> Scenaris => Set<GetScenari>();
    private IConfiguration _configuration;

    public ApplicationContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySQL(_configuration.GetConnectionString("MySql"));
    }
}