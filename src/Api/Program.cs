using System.Data.Common;
using System.Text;
using Api.Controller.AdminPanelSettings;
using Api.Data.Repository.MicroController;
using Api.Data.Repository.Music;
using Api.Data.Repository.PlayList;
using Api.Data.Repository.Scenari;
using Api.Data.Repository.User;
using Api.Data.Repository.UserRole;
using Api.Services.LdapConnectService;
using Api.Services.MicroControllerServices;
using Api.Services.MusicServices;
using Api.Services.PlayListServices;
using Api.Services.SettingsScenariServices;
using Api.Services.UserServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MySql.Data.MySqlClient;
using Radio.Model.JwtTokenConfig;
using Radio.Services.GeneratorTokenServices;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("Radio", policyBuilder =>
    {
        policyBuilder.AllowAnyHeader();
        policyBuilder.AllowAnyMethod();
        policyBuilder.AllowAnyOrigin();
    });
});


JwtTokenConfig jwtTokenConfig = new JwtTokenConfig();
builder.Services.AddSingleton(jwtTokenConfig);
builder.Services.AddAuthorizationBuilder()
    .AddPolicy("admin", policyBuilder =>
        policyBuilder.RequireRole("admin"));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme
).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidAudience = jwtTokenConfig.Audience,
        ValidIssuer = jwtTokenConfig.Issuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtTokenConfig.Secret)),
    };
});
builder.Services.AddTransient<MySqlConnection>();
builder.Services.AddTransient<MySqlCommand>();

builder.Services.AddScoped<IMicroControllerServices, MicroControllerServices>();
builder.Services.AddScoped<IMicroControllerRepository, MicroControllerRepository>();
builder.Services.AddScoped<IScenarioServices, ScenarioServices>();
builder.Services.AddScoped<IScenarioRepository, ScenarioRepository>();
builder.Services.AddScoped<IAdminPanelSettingsController, AdminPanelSettingsController>();
builder.Services.AddScoped<IMusicServices, MusicServices>();
builder.Services.AddScoped<IMusicRepository, MusicRepository>();
builder.Services.AddScoped<IUserRoleRepository, UserRoleRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserServices, UserServices>();
builder.Services.AddScoped<IPlayListRepository, PlayListRepository>();
builder.Services.AddScoped<IPlayListServices, PlayListServices>();
builder.Services.AddScoped<IGeneratorTokenServices, GeneratorTokenServices>();
builder.Services.AddScoped<ILdapService, LdapService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1"); });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseAuthentication();
app.MapControllers();

app.UseExceptionHandler(error => { error.Run(async context => { }); });

app.UseCors("Radio");

app.Run();