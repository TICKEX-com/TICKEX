using authentication_service.Data;
using authentication_service.Entities;
using authentication_service.Extensions;
using authentication_service.Services;
using authentication_service.Services.IServices;
using AutoMapper;
using Confluent.Kafka;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
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


builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("ApiSettings:JwtOptions"));

/*builder.Services.Configure<JwtOptions>(options =>
{
    options.Issuer = Environment.GetEnvironmentVariable("ISSUER") ?? string.Empty;
    options.Audience = Environment.GetEnvironmentVariable("AUDIENCE") ?? string.Empty;
    options.Secret = Environment.GetEnvironmentVariable("SECRET") ?? string.Empty;
});*/

builder.Services.AddDiscoveryClient();

// Auto Mapper
IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


// Add new columns 
builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<DataContext>().AddDefaultTokenProviders();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IProducerService, ProducerService>();

// Add HttpContextAccessor for Cookie 
builder.Services.AddHttpContextAccessor();

// Kafka Configuration Producer
var producerConfig = new ProducerConfig();
builder.Configuration.Bind("producer", producerConfig);
builder.Services.AddSingleton(producerConfig);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

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

builder.AddAppAuthentication();
builder.Services.AddAuthorization();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {

// }

// app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

ApplyMigration();

app.Run();

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
            Console.WriteLine(ex.Message);
        }

    }
}