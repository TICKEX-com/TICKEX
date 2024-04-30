using AutoMapper;
using Confluent.Kafka;
using event_service.Data;
using event_service.Extensions;
using event_service.Protos;
using event_service.Services;
using event_service.Services.IServices;
using Grpc.Net.Client.Web;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Steeltoe.Discovery.Client;


var builder = WebApplication.CreateBuilder(args);

/*builder.Services
    .AddScoped<IGreeterService, GreeterService>()
    .AddGrpcClient<Greeter.GreeterClient>((services, options) =>
    {
        options.Address = new Uri("http://localhost:5263");
    })
    .ConfigurePrimaryHttpMessageHandler(() =>
    {
        var handler = new HttpClientHandler();
        handler.ServerCertificateCustomValidationCallback =
            HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

        return handler;
    });*/

// Add Database

/*var dbHost = "127.0.0.1,1434";
var dbName = "Events";
var dbPassword = "1234Strong!Password";*/

var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
var dbName = Environment.GetEnvironmentVariable("DB_NAME");
var dbPassword = Environment.GetEnvironmentVariable("DB_SA_PASSWORD");

var connectionString = $"Data Source={dbHost};Database={dbName};User ID=sa;Password={dbPassword};Connect Timeout=10;Encrypt=False;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False";

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(connectionString);
});

builder.Services.AddControllers();

// Add services to the container.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDiscoveryClient();



builder.Services.AddSwaggerGen(option =>
{
    option.AddSecurityDefinition(name: JwtBearerDefaults.AuthenticationScheme, securityScheme: new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Enter the Bearer Authorization string as following: Bearer Generated-JWT-Token",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference= new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id=JwtBearerDefaults.AuthenticationScheme
                }
            }, new string[]{}
        }
    });
});

// Services config
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<IUserService, UserService>();




// Mapper
IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.AddAppAuthentication();
builder.Services.AddAuthorization();

var consumerConfig = new ConsumerConfig();
builder.Configuration.Bind("consumer", consumerConfig);
builder.Services.AddSingleton(consumerConfig);


// Consumer Configuration
builder.Services.AddHostedService<ConsumerService>();
// builder.Services.AddScoped<IScopedProcessingService, ScopedProcessingService>();




var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {

// }

// app.UseHttpsRedirection();
// ApplyMigration();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

ApplyMigration();

app.Run();



// Update database without using command line
void ApplyMigration()
{
    using (var scope = app.Services.CreateScope())
    {
        try
        {
            var _db = scope.ServiceProvider.GetRequiredService<DataContext>();

            if (_db.Database.GetPendingMigrations().Count() > 0)
            {
                _db.Database.EnsureDeleted();
                _db.Database.Migrate();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error in applying migrations : " + ex.Message);
        }

    }
}
