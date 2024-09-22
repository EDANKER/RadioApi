using System.Text;
using Api.Data.Minio;
using Api.Data.Repository.Admin;
using Api.Data.Repository.CacheRepository.DistributedCacheRepository;
using Api.Data.Repository.CacheRepository.HebrideanCacheRepository;
using Api.Data.Repository.MicroController;
using Api.Data.Repository.Music;
using Api.Data.Repository.PlayList;
using Api.Data.Repository.Scenario.ScenarioPlayRepository;
using Api.Data.Repository.Scenario.ScenarioTimeRepository;
using Api.Interface.CacheRepository;
using Api.Interface.MicroControllerServices;
using Api.Interface.Repository;
using Api.Model.JwtTokenConfig;
using Api.Model.RequestModel.Create.CreateMusic;
using Api.Model.RequestModel.Create.CreatePlayList;
using Api.Model.RequestModel.MicroController.FloorMicroController;
using Api.Model.RequestModel.Scenario.PlayScenario;
using Api.Model.RequestModel.Scenario.TimeScenario;
using Api.Model.RequestModel.Update.UpdateMusic;
using Api.Model.RequestModel.Update.UpdatePlayList;
using Api.Model.RequestModel.User;
using Api.Model.ResponseModel.FloorMicroController;
using Api.Model.ResponseModel.Music;
using Api.Model.ResponseModel.PlayList;
using Api.Model.ResponseModel.PlayScenario;
using Api.Model.ResponseModel.TimeScenario;
using Api.Model.ResponseModel.User;
using Api.Services.BackGroundTaskServices.MicroControllerGetServices;
using Api.Services.BackGroundTaskServices.ScenarioBackGroundServices;
using Api.Services.GeneratorTokenServices;
using Api.Services.HebrideanCacheServices;
using Api.Services.HttpMicroControllerServices;
using Api.Services.IFileServices;
using Api.Services.JsonServices;
using Api.Services.LdapService;
using Api.Services.LoginUserServices;
using Api.Services.MicroControllerServices;
using Api.Services.MusicServices;
using Api.Services.PlayListServices;
using Api.Services.RadioServices;
using Api.Services.ScenarioServices.ScenarioServicesPlay;
using Api.Services.ScenarioServices.ScnearioServicesTime;
using Api.Services.TimeCounterServices;
using Api.Services.UserServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Minio;
using MySql.Data.MySqlClient;

namespace Api.ConfigFile;

public static class ConfigFile
{
    public static void Registration(IServiceCollection service, string redis)
    {
        service.AddSingleton<IHebrideanCacheServices, HebrideanCacheServices>();
        service.AddSingleton<ICacheRepository, DistributedCacheRepository>();
        service.AddSingleton<HebrideanCacheRepository>();
        service.AddSingleton<IJsonServices<int[]>, JsonServices<int[]>>();
        service.AddSingleton<IJsonServices<string[]>, JsonServices<string[]>>();
        service.AddSingleton<IJsonServices<DtoPlayScenario>, JsonServices<DtoPlayScenario>>();
        service.AddSingleton<IJsonServices<DtoTimeScenario>, JsonServices<DtoTimeScenario>>();
        service.AddSingleton<IJsonServices<TimeScenario>, JsonServices<TimeScenario>>();
        service.AddSingleton<IJsonServices<PlayScenario>, JsonServices<PlayScenario>>();
        service.AddSingleton<IJsonServices<DtoMicroController>, JsonServices<DtoMicroController>>();
        service.AddSingleton<IJsonServices<MicroController>, JsonServices<MicroController>>();
        service.AddTransient<HttpClient>();
        service.AddTransient<MySqlConnection>();
        service.AddTransient<MySqlCommand>();
        service.AddTransient<IMinioClient, MinioClient>();
        service.AddSingleton<IMinio, Data.Minio.Minio>();
        service.AddSingleton<IFileServices, FileServices>();
        service.AddSingleton<IMicroControllerServices<MicroController, DtoMicroController>, MicroControllerFloorServices>();
        service.AddSingleton<IRepository<MicroController, DtoMicroController, MicroController>, MicroControllerFloorRepository>();
        service.AddSingleton<IScenarioTimeServices, ScenarioTimeServices>();
        service.AddSingleton<IScenarioPlayServices, ScenarioPlayServices>();
        service.AddSingleton<IRepository<TimeScenario, DtoTimeScenario, TimeScenario>, ScenarioTimeRepository>();
        service.AddSingleton<IRepository<PlayScenario, DtoPlayScenario, PlayScenario>, ScenarioPlayRepository>();
        service.AddSingleton<IMusicServices, MusicServices>();
        service.AddSingleton<IRepository<CreateMusic, DtoMusic, UpdateMusic>, MusicRepository>();
        service.AddSingleton<IRepository<User, DtoUser, User>, AdminPanelSettingsRepository>();
        service.AddSingleton<IUserServices, AdminPanelSettingsServices>();
        service.AddSingleton<IRepository<CreatePlayList, DtoPlayList, UpdatePlayList>, PlayListRepository>();
        service.AddSingleton<IPlayListServices, PlayListServices>();
        service.AddSingleton<IGeneratorTokenServices, GeneratorTokenServices>();
        service.AddSingleton<ILdapService, LdapService>();
        service.AddSingleton<ITimeCounterServices, TimeCounterServices>();
        service.AddSingleton<IHttpMicroControllerServices, HttpMicroControllerServices>();
        service.AddSingleton<ILoginUserServices, LoginUserServices>();
        service.AddSingleton<IRadioServices, RadioServices>();
        service.AddMemoryCache();
        service.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = redis;
            options.InstanceName = "Redis";
        });
        service.AddSingleton<IHostedService, ScenarioBackGroundServices>();
        service.AddSingleton<IHostedService, MicroControllerGetServices>();
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
}