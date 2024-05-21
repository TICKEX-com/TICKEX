using AutoMapper;
using Confluent.Kafka;
using event_service.Data;
using event_service.Extensions;
using event_service.Services;
using event_service.Services.IServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Steeltoe.Discovery.Client;


var builder = WebApplication.CreateBuilder(args);


// var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var connectionString = Environment.GetEnvironmentVariable("DefaultConnection");
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);


builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseNpgsql(connectionString);
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
builder.Services.AddScoped<IProducerService, ProducerService>();



// Mapper
IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.AddAppAuthentication();
builder.Services.AddAuthorization();

// Kafka Configuration for Consumer
var consumerConfig = new ConsumerConfig();
builder.Configuration.Bind("consumer", consumerConfig);
builder.Services.AddSingleton(consumerConfig);

// Kafka Configuration for Producer
var producerConfig = new ProducerConfig();
builder.Configuration.Bind("producer", producerConfig);
builder.Services.AddSingleton(producerConfig);

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
