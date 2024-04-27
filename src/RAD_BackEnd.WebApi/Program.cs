using Microsoft.EntityFrameworkCore;
using RAD_BackEnd.DataAccess.Contexts;
using RAD_BackEnd.DataAccess.Repositories;
using RAD_BackEnd.DataAccess.UnintOfWorks;
using RAD_BackEnd.Services.NewFolder;
using RAD_BackEnd.Services.Services.Events;
using RAD_BackEnd.Services.Services.Goals;
using RAD_BackEnd.Services.Services.Habits;
using RAD_BackEnd.Services.Services.Notes;
using RAD_BackEnd.Services.Services.Tasks;
using RAD_BackEnd.Services.Services.Users;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddScoped<INoteService, NoteService>();
builder.Services.AddScoped<IHabitService, HabitService>();
builder.Services.AddScoped<IGoalService, GoalService>();
builder.Services.AddScoped<IEventService, EventService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
