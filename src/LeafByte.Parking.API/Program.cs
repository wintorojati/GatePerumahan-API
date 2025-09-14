using LeafByte.Parking.API.Features.Cards;
using LeafByte.Parking.API.Features.Devices;
using LeafByte.Parking.API.Features.EntryLogs;
using LeafByte.Parking.API.Features.Gates;
using LeafByte.Parking.API.Features.Persons;
using LeafByte.Parking.API.Features.Users;
using LeafByte.Parking.API.Features.Auth;
using LeafByte.Parking.CrossCutting.Exceptions.Handler;
using LeafByte.Parking.API.Services;
using LeafByte.Parking.API.Data;
using LeafByte.Parking.API.Data.Seed;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using Serilog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var assembly = typeof(Program).Assembly;

builder.Services.AddValidatorsFromAssembly(assembly, includeInternalTypes: true);

builder.Host.UseSerilog((context, loggerConfiguration) =>
    loggerConfiguration.ReadFrom.Configuration(context.Configuration));

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));
builder.Services.AddAutoMapper(assembly);

builder.Services.AddScoped<AuthService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddOpenApi();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    await db.Database.MigrateAsync();

    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
    await VisitorCardSeeder.SeedAsync(db, app.Configuration, logger);
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");
app.UseExceptionHandler(options => { });

app.UseAuthentication();
app.UseAuthorization();

// Register grouped endpoints
app.AddDeviceEndpoints();
app.AddGateEndpoints();
app.AddPersonEndpoints();
app.AddUserEndpoints();
app.AddCardEndpoints();
app.AddEntryLogEndpoints();
app.AddAuthEndpoints();

app.MapOpenApi();
app.MapScalarApiReference();

app.Run();