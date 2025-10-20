using AppvilaAPI.Helpers.Middleware;
using Domain.Common;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpOverrides;
using Serilog;
using Service;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Enable CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Add secrets and configuration
builder.Configuration.AddUserSecrets(Assembly.GetExecutingAssembly(), true);

// Configure forwarded headers
builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
});

builder.Services.AddScoped<IUserContext, UserContext>();
builder.Services.AddServicesInfraStructure(builder.Configuration);
builder.Services.AddScoped<CustomMiddleware>();

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Host.UseSerilog(Log.Logger);

// Configure API behavior
builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
{
    options.SuppressModelStateInvalidFilter = true;
    options.SuppressConsumesConstraintForFormFileParameters = true;
});

// Swagger configuration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new()
    {
        Title = "Appvila API",
        Version = "v1",
        Description = ".NET 8.0 API for Appvila",
        Contact = new()
        {
            Email = "laithalshamasneh@hotmail.com",
            Name = "SHMX Software",
            Url = new("mailto:laithalshamasneh@hotmail.com")
        },
    });
});

// Configure form limits
builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = long.MaxValue;
});

var app = builder.Build();

app.UseDeveloperExceptionPage();

app.UseSwagger(c =>
{
    c.RouteTemplate = "swagger/{documentName}/swagger.json";
});
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("../swagger/v1/swagger.json", "Agency API");
    c.InjectStylesheet("../swagger-ui/custom.css");
    c.DisplayOperationId();
    c.DisplayRequestDuration();
    c.DefaultModelsExpandDepth(-1);
});

app.UseForwardedHeaders();
app.UseCors("AllowAll");
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles();

app.UseMiddleware<CustomMiddleware>();

app.MapControllers();

app.Run();
