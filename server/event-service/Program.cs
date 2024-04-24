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

builder.Services.AddControllers();
// Add Database

/*var dbHost = "localhost";
var dbName = "Events";
var dbPassword = "1234Strong!Password";*/

var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
var dbName = Environment.GetEnvironmentVariable("DB_NAME");
var dbPassword = Environment.GetEnvironmentVariable("DB_SA_PASSWORD");

var connectionString = $"Data Source={dbHost};Initial Catalog={dbName};User ID=sa;Password={dbPassword};Connect Timeout=100;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(connectionString);
});


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
builder.AddAppAuthetication();
builder.Services.AddAuthorization();

// Consumer Configuration
builder.Services.AddHostedService<ConsumerService>();
// builder.Services.AddScoped<IScopedProcessingService, ScopedProcessingService>();

var consumerConfig = new ConsumerConfig();
builder.Configuration.Bind("consumer", consumerConfig);
builder.Services.AddSingleton(consumerConfig);


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

async void Consume()
{
    ConsumerConfig _consumerConfig;

    using (var consumer = new ConsumerBuilder<Ignore, string>(consumerConfig).Build())
    {
        try
        {
            consumer.Subscribe("Tickex");

            CancellationTokenSource cts = new CancellationTokenSource();
            Console.CancelKeyPress += (_, e) => {
                e.Cancel = true; // prevent the process from terminating.
                cts.Cancel();
            };

            while (!cts.IsCancellationRequested)
            {
                try
                {
                    var cr = consumer.Consume(cts.Token);
                    Console.WriteLine($"Consumed event from Tickex : key = {cr.Message.Key,-10} value = {cr.Message.Value}");

                }
                catch (ConsumeException e)
                {
                    Console.WriteLine($"Consumer Error occured: {e.Error.Reason}");
                }
            }
        }
        catch (OperationCanceledException)
        {
            // Ctrl + C
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Subscribe Error occured: {ex.Message}");
        }
        finally
        {
            consumer.Close();
        }
    }
}