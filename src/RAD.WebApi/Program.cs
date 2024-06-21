using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using RAD.DAL.Contexts;
using RAD.WebApi.Extensions;
using RAD.WebApi.Helpers;
using RAD.WebApi.Mappers;
using Serilog;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
    options.Conventions.Add(new RouteTokenTransformerConvention(new RouteHelper()))
)
.AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
})
.AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

// Configure database context
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configure Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSwagger();

// Configure Serilog
builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

// Add custom services
builder.Services.AddJWtService(builder.Configuration);
builder.Services.AddExceptionHandlers();
builder.Services.AddProblemDetails();
builder.Services.AddMemoryCache();
builder.Services.AddHttpContextAccessor();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddValidators();
builder.Services.AddApiServices();
builder.Services.AddServices();

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder
            .WithOrigins("https://road-to-the-dream.uz", "http://localhost:5500")
            .AllowAnyHeader()
            .AllowAnyMethod());
});

var app = builder.Build();

// Inject environment items
app.InjectEnvironmentItems();

// Ensure database is created and migrated
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<AppDbContext>();

    dbContext.Database.EnsureCreated();
    dbContext.Database.Migrate();
}

// Configure middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler();

app.UseCors("AllowSpecificOrigin");

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
