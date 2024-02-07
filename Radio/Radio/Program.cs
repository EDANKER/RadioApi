using System.Configuration;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Radio.Controller.AdminPanel.AdminPanelSettings;
using Radio.Data.Repository;
using Radio.Data.Repository.PlayList;
using Radio.Data.Repository.Scenari;
using Radio.Data.Repository.User;
using Radio.Model.JwtTokenConfig;
using Radio.Model.ResponseModel.Scenari;
using Radio.Services.AdminPanelServices;
using Radio.Services.GeneratorTokenServices;
using Radio.Services.LdapConnectService;
using Radio.Services.MusicServices;
using Radio.Services.PlayListServices;
using Radio.Services.SettingsScenariServices;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("User", policyBuilder =>
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

builder.Services.AddScoped<IScenariServices, ScenariServices>();
builder.Services.AddScoped<IScenariRepository, ScenariRepository>();
builder.Services.AddScoped<IAdminPanelSettingsController, AdminPanelSettingsController>();
builder.Services.AddScoped<IMusicServices, MusicServices>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserServices, UserServices>();
builder.Services.AddScoped<IMusicRepository, MusicRepository>();
builder.Services.AddScoped<IPlayListRepository, PlayListRepository>();
builder.Services.AddScoped<IPlayListServices, PlayListServices>();
builder.Services.AddScoped<IGeneratorTokenServices, GeneratorTokenServices>();
builder.Services.AddScoped<ILdapConnectService, LdapConnectServiceService>();
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

app.UseCors("User");

app.Run();