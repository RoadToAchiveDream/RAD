using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using RAD_BackEnd.DataAccess.Contexts;
using RAD_BackEnd.Services.NewFolder;
using RAD_BackEnd.WebApi.Extensions;
using RAD_BackEnd.WebApi.Helpers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
    options.Conventions.Add(new RouteTokenTransformerConvention(new RouteHelper())));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGenJwt();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddAuthorization();
builder.Services.AddJWtService(builder.Configuration);

builder.Services.AddServices();
builder.Services.AddValidators();
builder.Services.AddMemoryCache();

EnvironmentHelper.WebRootPath = builder.Environment.WebRootPath;

var app = builder.Build();
app.InjectEnvironmentItems();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<AppDbContext>();

    dbContext.Database.EnsureCreated();
    dbContext.Database.Migrate();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.Run();
