using System.Text;
using Api.Data.Minio;
using Api.Data.Repository.CacheRepository;
using Api.Data.Repository.CacheRepository.DistributedCacheRepository;
using Api.Data.Repository.CacheRepository.HebrideanCacheRepository;
using Api.Data.Repository.CacheRepository.MemoryCacheRepository;
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
using Api.Services.GeneratorTokenServices;
using Api.Services.HebrideanCacheServices;
using Api.Services.HttpMicroControllerServices;
using Api.Services.IFileServices;
using Api.Services.JsonServices;
using Api.Services.LdapService;
using Api.Services.MicroControllerServices;
using Api.Services.MusicPlayerToMicroControllerServices;
using Api.Services.MusicServices;
using Api.Services.PlayListServices;
using Api.Services.ScenarioServices;
using Api.Services.StreamToByteArrayServices;
using Api.Services.TimeCounterServices;
using Api.Services.UserServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;
using Minio;
using MySql.Data.MySqlClient;

namespace Api.ConfigFile;

public static class ConfigFile
{
    public static void Registration(IServiceCollection service)
    {
        service.AddScoped<IHebrideanCacheServices, HebrideanCacheServices>();
        service.AddScoped<ICacheRepository, MemoryCacheRepository>();
        service.AddScoped<ICacheRepository, DistributedCacheRepository>();
        service.AddScoped<HebrideanCacheRepository>();
        service.AddScoped<IJsonServices<int[]>, JsonServices<int[]>>();
        service.AddScoped<IJsonServices<string[]>, JsonServices<string[]>>();
        service.AddScoped<IJsonServices<DtoMicroController>, JsonServices<DtoMicroController>>();
        service.AddScoped<IStreamToByteArrayServices, StreamToByteArrayServices>();
        service.AddTransient<MySqlConnection>();
        service.AddTransient<MySqlCommand>();
        service.AddTransient<IMinioClient, MinioClient>();
        service.AddScoped<IMinio, Data.Minio.Minio>();
        service.AddScoped<IFileServices, FileServices>();
        service.AddScoped<IMusicPlayerToMicroControllerServices, MusicPlayerToMicroControllerServices>();
        service.AddScoped<IMicroControllerServices, MicroControllerServices>();
        service.AddScoped<IRepository<MicroController, DtoMicroController, MicroController>, MicroControllerRepository>();
        service.AddScoped<IScenarioServices, ScenarioServices>();
        service.AddScoped<IRepository<Scenario, DtoScenario, Scenario>, ScenarioRepository>();
        service.AddScoped<IMusicServices, MusicServices>();
        service.AddScoped<IRepository<CreateMusic, DtoMusic, UpdateMusic>, MusicRepository>();
        service.AddScoped<IRepository<User, DtoUser, User>, UserRepository>();
        service.AddScoped<IUserServices, UserServices>();
        service.AddScoped<IRepository<CreatePlayList, DtoPlayList, UpdatePlayList>, PlayListRepository>();
        service.AddScoped<IPlayListServices, PlayListServices>();
        service.AddScoped<IGeneratorTokenServices, GeneratorTokenServices>();
        service.AddScoped<ILdapService, LdapService>();
        service.AddScoped<ITimeCounterServices, TimeCounterServices>();
        service.AddScoped<IHttpMicroControllerServices, HttpMicroControllerServices>();
        service.AddScoped<HttpClient>();
        service.AddMemoryCache();
        service.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = "http://10.3.15.204";
            options.InstanceName = "Redis";
        });
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