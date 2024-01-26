using Microsoft.EntityFrameworkCore;
using Radio.Model.User;

namespace Radio.Data.ApplicationContext;

public class ApplicationContext : DbContext
{
    public DbSet<User> DbSet => Set<User>();
    private IConfiguration _configuration;

    public ApplicationContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public ApplicationContext(DbContextOptions<ApplicationContext> options)
    {
       
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySQL(_configuration.GetConnectionString("MySql"));
    }
}