using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using NotificationService.Application.Repositories;
using NotificationService.Application.Services;
using NotificationService.Configuration;
using NotificationService.Contracts.Data;
using NotificationService.Infrastructure;
using NotificationService.Infrastructure.HostedServices;
using NotificationService.Infrastructure.Hubs;
using NotificationService.Infrastructure.Repositories;
using NotificationService.Infrastructure.Services;
using System.Reflection;
using System.Text.Json.Serialization;
using Serilog;

var AllowAllOrigins = "_AllowAllOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true, reloadOnChange: true);

builder.Services.AddCors(options =>
{
    options.AddPolicy(
        AllowAllOrigins,
        builder =>
        {
            builder
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

builder.Services.AddDbContext<NotificationDbContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

builder.Host.UseSerilog((context, loggerConfig) =>
    loggerConfig.ReadFrom.Configuration(context.Configuration));

var firebaseProjectId = builder.Configuration["FirebaseAuthClientConfig:ProjectId"];

var tokenValidationParameters = new TokenValidationParameters
{
    ValidateIssuer = true,
    ValidIssuer = "https://securetoken.google.com/" + firebaseProjectId,
    ValidateAudience = true,
    ValidAudience = firebaseProjectId,
    ValidateLifetime = true,
};

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = "https://securetoken.google.com/" + firebaseProjectId;
        options.TokenValidationParameters = tokenValidationParameters;

        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                var accessToken = context.Request.Query["access_token"];
                var path = context.HttpContext.Request.Path;
                if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/notificationHub"))
                {
                    var hubInput = JsonConvert.DeserializeObject<HubAccessInput>(accessToken!);
                    context.Token = hubInput!.Token;
                }

                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddHttpContextAccessor();

builder.Services.Configure<FirebaseAuthClientConfig>(builder.Configuration.GetSection("FirebaseAuthClientConfig"));
builder.Services.Configure<RabbitMQConfig>(builder.Configuration.GetSection("RabbitMQConfig"));

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

if (!string.IsNullOrWhiteSpace(builder.Configuration.GetSection("ElasticApm").GetValue<string>("ServerCert")))
{
    builder.Services.AddElasticApm();
}

builder.Services.AddRouting(options =>
{
    options.LowercaseUrls = true;
    options.LowercaseQueryStrings = true;
});

builder.Services.AddScoped<INotificationService, NotificationService.Infrastructure.Services.NotificationService>();
builder.Services.AddScoped<INotificationSettingService, NotificationSettingService>();

builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
builder.Services.AddScoped<INotificationSettingRepository, NotificationSettingRepository>();

builder.Services.AddSingleton<INotificationReceiverService, NotificationReceiverService>();

builder.Services.AddSignalR();

builder.Services.AddHostedService<NotificationReceiverListenerHostedService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseSwagger((opt) =>
{
    opt.RouteTemplate = "swagger/{documentName}/swagger.json";
});

app.UseSwaggerUI();

app.UseMiddleware<RequestContextLoggingMiddleware>();
app.UseSerilogRequestLogging();

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseRouting();
app.UseCors(AllowAllOrigins);

app.UseAuthentication();

app.UseAuthorization();

app.UseHttpsRedirection();


app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHub<NotificationHub>("/notificationHub");
});

app.MapDefaultControllerRoute();

using var scope = app.Services.CreateScope();

var context = scope.ServiceProvider.GetRequiredService<NotificationDbContext>();

context.Database.Migrate();

app.Run();
