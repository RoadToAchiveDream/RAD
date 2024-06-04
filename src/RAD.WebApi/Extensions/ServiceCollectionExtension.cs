using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RAD.DAL.UnintOfWorks;
using RAD.Services.Helpers;
using RAD.Services.Services.Events;
using RAD.Services.Services.Goals;
using RAD.Services.Services.Habits;
using RAD.Services.Services.Notes;
using RAD.Services.Services.Tasks;
using RAD.Services.Services.Users;
using RAD.WebApi.ApiServices.Events;
using RAD.WebApi.ApiServices.Goals;
using RAD.WebApi.ApiServices.Habits;
using RAD.WebApi.ApiServices.Notes;
using RAD.WebApi.ApiServices.Tasks;
using RAD.WebApi.ApiServices.Users;
using RAD.WebApi.Middlewares;
using RAD.WebApi.Validators.Events;
using RAD.WebApi.Validators.Goals;
using RAD.WebApi.Validators.Habits;
using RAD.WebApi.Validators.Notes;
using RAD.WebApi.Validators.Tasks;
using RAD.WebApi.Validators.Users;
using System.Text;

namespace RAD.WebApi.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ITaskService, TaskService>();
        services.AddScoped<INoteService, NoteService>();
        services.AddScoped<IHabitService, HabitService>();
        services.AddScoped<IGoalService, GoalService>();
        services.AddScoped<IEventService, EventService>();
    }

    public static void AddApiServices(this IServiceCollection services)
    {
        services.AddScoped<IUserApiService, UserApiService>();

        services.AddScoped<INoteApiService, NoteApiService>();

        services.AddScoped<ITaskApiService, TaskApiService>();

        services.AddScoped<IGoalApiService, GoalApiService>();

        services.AddScoped<IHabitApiService, HabitApiService>();

        services.AddScoped<IEventApiService, EventApiService>();
    }

    public static void AddValidators(this IServiceCollection services)
    {
        services.AddTransient<UserCreateModelValidator>();
        services.AddTransient<UserUpdateModelValidator>();
        services.AddTransient<UserChangePasswordModelValidator>();

        services.AddTransient<NoteCreateModelValidator>();
        services.AddTransient<NoteUpdateModelValidator>();

        services.AddTransient<TaskCreateModelValidator>();
        services.AddTransient<TaskUpdateModelValidator>();

        services.AddTransient<EventCreateModelValidator>();
        services.AddTransient<EventUpdateModelValidator>();

        services.AddTransient<GoalCreateModelValidator>();
        services.AddTransient<GoalUpdateModelValidator>();

        services.AddTransient<HabitCreateModelValidator>();
        services.AddTransient<HabitUpdateModelValidator>();
    }


    public static void AddExceptionHandlers(this IServiceCollection services)
    {
        services.AddExceptionHandler<NotFoundExceptionHandler>();
        services.AddExceptionHandler<AlreadyExistExceptionHandler>();
        services.AddExceptionHandler<ArgumentIsNotValidExceptionHandler>();
        services.AddExceptionHandler<CustomExceptionHandler>();
        services.AddExceptionHandler<InternalServerExceptionHandler>();
    }

    public static void InjectEnvironmentItems(this WebApplication app)
    {
        HttpContextHelper.ContextAccessor = app.Services.GetRequiredService<IHttpContextAccessor>();
        EnvironmentHelper.WebRootPath = Path.GetFullPath("wwwroot");
        EnvironmentHelper.JWTKey = app.Configuration.GetSection("JWT:Key").Value;
        EnvironmentHelper.TokenLifeTimeInHours = app.Configuration.GetSection("JWT:LifeTime").Value;
        EnvironmentHelper.EmailAddress = app.Configuration.GetSection("Email:EmailAddress").Value;
        EnvironmentHelper.EmailPassword = app.Configuration.GetSection("Email:Password").Value;
        EnvironmentHelper.SmtpPort = app.Configuration.GetSection("Email:Port").Value;
        EnvironmentHelper.SmtpHost = app.Configuration.GetSection("Email:Host").Value;
        EnvironmentHelper.PageSize = Convert.ToInt32(app.Configuration.GetSection("PaginationParams:PageSize").Value);
        EnvironmentHelper.PageIndex = Convert.ToInt32(app.Configuration.GetSection("PaginationParams:PageIndex").Value);
    }

    public static void AddJWtService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(o =>
        {
            var key = Encoding.UTF8.GetBytes(configuration["JWT:Key"]);
            o.SaveToken = true;
            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["JWT:Issuer"],
                ValidAudience = configuration["JWT:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };
        });
    }

    public static void ConfigureSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(setup =>
        {
            var jwtSecurityScheme = new OpenApiSecurityScheme
            {
                BearerFormat = "JWT",
                Name = "JWT Authentication",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = JwtBearerDefaults.AuthenticationScheme,
                Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

                Reference = new OpenApiReference
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme
                }
            };

            setup.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

            setup.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { jwtSecurityScheme, Array.Empty<string>() }
                });
        });
    }
}
