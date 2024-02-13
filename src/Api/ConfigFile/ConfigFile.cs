using System.Text;
using Api.Controller.AdminPanelSettings;
using Api.Data.Repository.MicroController;
using Api.Data.Repository.Music;
using Api.Data.Repository.PlayList;
using Api.Data.Repository.Scenari;
using Api.Data.Repository.User;
using Api.Data.Repository.User.UserRole;
using Api.DTO.PlayList;
using Api.Model.JwtTokenConfig;
using Api.Model.ResponseModel.PlayList;
using Api.Profile.AppMappingProfile;
using Api.Services.LdapService;
using Api.Services.MicroControllerServices;
using Api.Services.MusicServices;
using Api.Services.PlayListServices;
using Api.Services.SaveAudioFile;
using Api.Services.SettingsScenariServices;
using Api.Services.TransmissionToMicroController;
using Api.Services.UserServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using MySql.Data.MySqlClient;
using Radio.Services.GeneratorTokenServices;

namespace Api.ConfigFile;

public static class ConfigFile
{
    public static void Registration(IServiceCollection service)
    {
        service.AddTransient<MySqlConnection>();
        service.AddTransient<MySqlCommand>();
        service.AddTransient<MongoClient>();
        
        service.AddScoped<ITransmissionToMicroController, TransmissionToMicroController>();
        service.AddScoped<ISaveAudioFileServices, SaveAudioFileServicesServices>();
        service.AddScoped<IMicroControllerServices, MicroControllerServices>();
        service.AddScoped<IMicroControllerRepository, MicroControllerRepository>();
        service.AddScoped<IScenarioServices, ScenarioServices>();
        service.AddScoped<IScenarioRepository, ScenarioRepository>();
        service.AddScoped<IAdminPanelSettingsController, AdminPanelSettingsController>();
        service.AddScoped<IMusicServices, MusicServices>();
        service.AddScoped<IMusicRepository, MusicRepository>();
        service.AddScoped<IUserRoleRepository, UserRoleRepository>();
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
        app.UseExceptionHandler(error =>
        {
            error.Run(async context =>
            {
                
            }); 
        });
    }

    public static void AutoMapper(IServiceCollection service)
    {
        
    }
}