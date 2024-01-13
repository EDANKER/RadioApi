using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo());
});
builder.Services.AddCors(options => options.AddPolicy("Corssettings", policyBuilder => policyBuilder
    .WithOrigins("mydomen.com")
    .WithHeaders("Id")));

var app = builder.Build();

app.UseCors("Corssettings");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run(async context =>
{
    await context.Response.WriteAsync("Hello");
});

app.Run();