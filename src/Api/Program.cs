using Api.ConfigFile;

var builder = WebApplication.CreateBuilder(args);

ConfigFile.Cors(builder.Services);
ConfigFile.Jwt(builder.Services);
var value = builder.Configuration.GetSection("Redis:url").Value;
if (value != null)
    ConfigFile.Registration(builder.Services, value);

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

app.UseCors("Radio");

app.Run();