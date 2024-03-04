using System.Text;
using Api.Controller.AdminPanelSettings;
using Api.Data.Minio;
using Api.Data.Repository.MicroController;
using Api.Data.Repository.Music;
using Api.Data.Repository.PlayList;
using Api.Data.Repository.Scenario;
using Api.Data.Repository.User;
using Api.Model.JwtTokenConfig;
using Api.Services.AudioFileSaveToMicroControllerServices;
using Api.Services.GeneratorTokenServices;
using Api.Services.LdapService;
using Api.Services.MicroControllerServices;
using Api.Services.MusicPlayerToMicroControllerServices;
using Api.Services.MusicServices;
using Api.Services.PlayListServices;
using Api.Services.ScenarioServices;
using Api.Services.UserServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Minio;
using MySql.Data.MySqlClient;

namespace Api.ConfigFile;

public static class ConfigFile
{
    public static void Registration(IServiceCollection service)
    {
        service.AddTransient<MySqlConnection>();
        service.AddTransient<MySqlCommand>();
        service.AddTransient<IMinioClient, MinioClient>();
        service.AddScoped<IMinio, Data.Minio.Minio>();
        service.AddScoped<IAudioFileSaveToMicroControllerServices, AudioFileSaveToMicroControllerServices>();
        service.AddScoped<IMusicPlayerToMicroControllerServices, MusicPlayerToMicroControllerServices>();
        service.AddScoped<IMicroControllerServices, MicroControllerServices>();
        service.AddScoped<IMicroControllerRepository, MicroControllerRepository>();
        service.AddScoped<IScenarioServices, ScenarioServices>();
        service.AddScoped<IScenarioRepository, ScenarioRepository>();
        service.AddScoped<IAdminPanelSettingsController, AdminPanelSettingsController>();
        service.AddScoped<IMusicServices, MusicServices>();
        service.AddScoped<IMusicRepository, MusicRepository>();
        service.AddScoped<IUserRepository, UserRepository>();
        service.AddScoped<IUserServices, UserServices>();
        service.AddScoped<IPlayListRepository, PlayListRepository>();
        service.AddScoped<IPlayListServices, PlayListServices>();
        service.AddScoped<IGeneratorTokenServices, GeneratorTokenServices>();
        service.AddScoped<ILdapService, LdapService>();
    }

    public static void Jwt(IServiceCollection service)
    {
        service.AddAuthentication(JwtBearerDefaults.AuthenticationScheme
        ).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidAudience = JwtTokenConfig.Audience,
                ValidIssuer = JwtTokenConfig.Issuer,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtTokenConfig.Secret)),
            };
        });
    }

    public static void Cors(IServiceCollection service)
    {
        service.AddCors(options =>
        {
            options.AddPolicy("Radio", policyBuilder =>
            {
                policyBuilder.AllowAnyHeader();
                policyBuilder.AllowAnyMethod();
                policyBuilder.AllowAnyOrigin();
            });
        });
    }

    public static void Exception(WebApplication app)
    {
        
    }
}