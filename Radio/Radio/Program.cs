using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Radio.Data.Repository;
using Radio.Data.Repository.PlayList;
using Radio.Model.JwtTokenConfig;
using Radio.Services.GeneratorTokenServices;
using Radio.Services.LdapConnectService;
using Radio.Services.MusicServices;
using Radio.Services.PlayListServices;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();

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
builder.Services.AddScoped<IMusicServices, MusicServices>();
builder.Services.AddScoped<IMusicRepository, MusicRepository>();
builder.Services.AddScoped<IPlayListRepository, PlayListRepository>();
builder.Services.AddScoped<IPlayListServices, PlayListServices>();
builder.Services.AddScoped<IGeneratorTokenServices, GeneratorTokenServices>();
builder.Services.AddSingleton<ILdapConnectService, LdapConnectServiceService>();
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

app.Run();