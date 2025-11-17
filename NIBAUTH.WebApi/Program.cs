using Asp.Versioning;
using NIBAUTH.Application;
using NIBAUTH.Application.Common.Mappings;
using NIBAUTH.Application.Common.Utilities;
using NIBAUTH.Application.Interfaces;
using NIBAUTH.Domain.Entities;
using NIBAUTH.WebApi;
using NIBAUTH.WebApi.Middleware;
using NIBAUTH.WebApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using System.Text;
using System.Text.Json;
using NIBAUTH.Persistence;
using Microsoft.EntityFrameworkCore;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File("Logs/NIBAUTHWebApi-.log", rollingInterval: RollingInterval.Day)
    .CreateLogger();

try
{
    Log.Information(" NIBAUTH WebAPI Starting...");

    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog();

    // ---------------------------- Database ----------------------------
    builder.Services.AddDbContext<NIBAUTHDbContext>(options =>
        options.UseNpgsql(builder.Configuration["NIBAUTHDatabase"]));

    // ---------------------------- Identity ----------------------------
    builder.Services.AddIdentityCore<User>(options =>
    {
        options.User.RequireUniqueEmail = false;
        options.SignIn.RequireConfirmedEmail = false;
    })
 .AddRoles<Role>()
 .AddEntityFrameworkStores<NIBAUTHDbContext>()
 .AddDefaultTokenProviders();


    // ---------------------------- JWT ----------------------------
    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ClockSkew = TimeSpan.Zero,
            ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
            ValidAudience = builder.Configuration["JwtSettings:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecretKey"]!))
        };
    });

    // ---------------------------- Authorization ----------------------------
    //builder.Services.AddAuthorization(options =>
    //{
    //    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin", "SuperAdmin"));
    //    options.AddPolicy("SuperAdminOnly", policy => policy.RequireRole("SuperAdmin"));
    //});

    // ---------------------------- AutoMapper ----------------------------
    builder.Services.AddAutoMapper(cfg =>
    {
        cfg.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
        cfg.AddProfile(new AssemblyMappingProfile(typeof(INIBAUTHDbContext).Assembly));
    });

    // ---------------------------- App Layers ----------------------------
    builder.Services.AddApplication();
    builder.Services.AddPersistence(builder.Configuration);
    builder.Services.AddMediatR(cfg =>
        cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

    // ---------------------------- Custom Services ----------------------------
    builder.Services.AddSingleton<TokenManager>();
    builder.Services.AddSingleton<ICurrentUserService, CurrentUserService>();



    builder.Services.AddHttpContextAccessor();
    builder.Services.AddHttpClient();

    // ---------------------------- Controllers ----------------------------
    builder.Services.AddControllers().AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    });

    // ---------------------------- API Versioning ----------------------------
    builder.Services.AddApiVersioning(options =>
    {
        options.DefaultApiVersion = new ApiVersion(1.0);
        options.AssumeDefaultVersionWhenUnspecified = true;
        options.ReportApiVersions = true;
        options.ApiVersionReader = new UrlSegmentApiVersionReader();
    })
    .AddApiExplorer(options =>
    {
        options.GroupNameFormat = "'v'VVV";
        options.SubstituteApiVersionInUrl = true;
    });

    // ---------------------------- Swagger ----------------------------
    builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(options =>
    {
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Description = "JWT Authorization header. Example: 'Bearer {token}'",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer"
        });
        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                },
                Array.Empty<string>()
            }
        });
    });

    // ---------------------------- CORS ----------------------------
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAll", policy =>
        {
            policy
                .SetIsOriginAllowed(origin => true)
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        });
    });

    builder.Services.AddSignalR();
    builder.Logging.ClearProviders();
    builder.Logging.AddConsole();




    var app = builder.Build();

    // ---------------------------- CLI Seeder ----------------------------
    if (args.Contains("seed"))
    {
        using var scope = app.Services.CreateScope();
        var config = builder.Configuration;
        var serviceProvider = scope.ServiceProvider;

        try
        {
            await DbInitializer.RunSeed(config, serviceProvider);
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, " CLI seeding failed.");
        }

        return;
    }

    // ---------------------------- Middleware ----------------------------
    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseStaticFiles(new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(Path.Combine(app.Environment.ContentRootPath, "static")),
        RequestPath = "/static"
    });

    app.UseRouting();
    app.UseCors("AllowAll");
    app.UseAuthentication();
    app.UseAuthorization();
    app.UseCustomExceptionHandler();
    app.UseDefaultFiles();
    app.UseStaticFiles();

    // ---------------------------- Endpoints ----------------------------
    app.MapGet("/", () => "NIBAUTH API is running");
    app.MapControllers();


    app.UseWebSockets();

    await app.RunAsync();
}
catch (Exception ex)
{
    Log.Fatal(ex, " Application start-up failed.");
}
finally
{
    Log.CloseAndFlush();
}

