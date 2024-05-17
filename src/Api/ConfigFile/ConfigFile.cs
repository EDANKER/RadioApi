using System.Text;
using Api.Data.Minio;
using Api.Data.Repository.CacheRepository;
using Api.Data.Repository.CacheRepository.DistributedCacheRepository;
using Api.Data.Repository.CacheRepository.HebrideanCacheRepository;
using Api.Data.Repository.MicroController;
using Api.Data.Repository.Music;
using Api.Data.Repository.PlayList;
using Api.Data.Repository.Scenario;
using Api.Data.Repository.User;
using Api.Interface;
using Api.Model.JwtTokenConfig;
using Api.Model.RequestModel.Create.CreateMusic;
using Api.Model.RequestModel.Create.CreatePlayList;
using Api.Model.RequestModel.MicroController;
using Api.Model.RequestModel.Scenario;
using Api.Model.RequestModel.Update.UpdateMusic;
using Api.Model.RequestModel.Update.UpdatePlayList;
using Api.Model.RequestModel.User;
using Api.Model.ResponseModel.MicroController;
using Api.Model.ResponseModel.Music;
using Api.Model.ResponseModel.PlayList;
using Api.Model.ResponseModel.Scenario;
using Api.Model.ResponseModel.User;
using Api.Services.BackGroundTaskServices.MicroControllerGetServices;
using Api.Services.BackGroundTaskServices.ScenarioTimeGetServices;
using Api.Services.GeneratorTokenServices;
using Api.Services.HebrideanCacheServices;
using Api.Services.HttpMicroControllerServices;
using Api.Services.IFileServices;
using Api.Services.JsonServices;
using Api.Services.LdapService;
using Api.Services.LoginUserServices;
using Api.Services.MicroControllerServices;
using Api.Services.MusicPlayerToMicroControllerServices;
using Api.Services.MusicServices;
using Api.Services.PlayListServices;
using Api.Services.ScenarioServices;
using Api.Services.StreamToByteArrayServices;
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
        service.AddSingleton<IJsonServices<DtoScenario>, JsonServices<DtoScenario>>();
        service.AddSingleton<IJsonServices<Scenario>, JsonServices<Scenario>>();
        service.AddSingleton<IJsonServices<DtoMicroController>, JsonServices<DtoMicroController>>();
        service.AddSingleton<IJsonServices<MicroController>, JsonServices<MicroController>>();
        service.AddSingleton<IStreamToByteArrayServices, StreamToByteArrayServices>();
        service.AddTransient<MySqlConnection>();
        service.AddTransient<MySqlCommand>();
        service.AddTransient<IMinioClient, MinioClient>();
        service.AddSingleton<IMinio, Data.Minio.Minio>();
        service.AddSingleton<IFileServices, FileServices>();
        service.AddSingleton<IMusicPlayerToMicroControllerServices, MusicPlayerToMicroControllerServices>();
        service.AddSingleton<IMicroControllerServices, MicroControllerServices>();
        service.AddSingleton<IRepository<MicroController, DtoMicroController, MicroController>, MicroControllerRepository>();
        service.AddSingleton<IScenarioServices, ScenarioServices>();
        service.AddSingleton<IRepository<Scenario, DtoScenario, Scenario>, ScenarioRepository>();
        service.AddSingleton<IMusicServices, MusicServices>();
        service.AddSingleton<IRepository<CreateMusic, DtoMusic, UpdateMusic>, MusicRepository>();
        service.AddSingleton<IRepository<User, DtoUser, User>, UserRepository>();
        service.AddSingleton<IUserServices, UserServices>();
        service.AddSingleton<IRepository<CreatePlayList, DtoPlayList, UpdatePlayList>, PlayListRepository>();
        service.AddSingleton<IPlayListServices, PlayListServices>();
        service.AddSingleton<IGeneratorTokenServices, GeneratorTokenServices>();
        service.AddSingleton<ILdapService, LdapService>();
        service.AddSingleton<ITimeCounterServices, TimeCounterServices>();
        service.AddSingleton<IHttpMicroControllerServices, HttpMicroControllerServices>();
        service.AddSingleton<ILoginUserServices, LoginUserServices>();
        service.AddSingleton<HttpClient>();
        service.AddMemoryCache();
        service.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = redis;
            options.InstanceName = "Redis";
        });
        service.AddSingleton<IHostedService, ScenarioTimeGetServices>();
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