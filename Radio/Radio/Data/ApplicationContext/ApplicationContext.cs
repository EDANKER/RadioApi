using Microsoft.EntityFrameworkCore;
using Radio.Model.Music;
using Radio.Model.PlayList;
using Radio.Model.ResponseModel.User;

namespace Radio.Data.ApplicationContext;

public class ApplicationContext : DbContext
{
    public DbSet<GetPlayList> PlayList => Set<GetPlayList>();
    public DbSet<GetMusic> Musics => Set<GetMusic>();
    public DbSet<GetUser> User => Set<GetUser>();
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