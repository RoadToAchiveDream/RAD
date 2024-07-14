using AutoMapper;
using RAD.Domain.Entities;
using RAD.WebApi.DTOs.Cashbooks;
using RAD.WebApi.DTOs.Events;
using RAD.WebApi.DTOs.Goals;
using RAD.WebApi.DTOs.Habits;
using RAD.WebApi.DTOs.NoteCategories;
using RAD.WebApi.DTOs.Notes;
using RAD.WebApi.DTOs.TaskCategories;
using RAD.WebApi.DTOs.Tasks;
using RAD.WebApi.DTOs.Users;
using Task = RAD.Domain.Entities.Task;

namespace RAD.WebApi.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserCreateModel>().ReverseMap();
        CreateMap<User, UserUpdateModel>().ReverseMap();
        CreateMap<User, UserViewModel>().ReverseMap();

        CreateMap<Task, TaskCreateModel>().ReverseMap();
        CreateMap<Task, TaskUpdateModel>().ReverseMap();
        CreateMap<Task, TaskViewModel>().ReverseMap();

        CreateMap<TaskCategory, TaskCategoryCreateModel>().ReverseMap();
        CreateMap<TaskCategory, TaskCategoryUpdateModel>().ReverseMap();
        CreateMap<TaskCategory, TaskCategoryViewModel>().ReverseMap();

        CreateMap<Note, NoteCreateModel>().ReverseMap();
        CreateMap<Note, NoteUpdateModel>().ReverseMap();
        CreateMap<Note, NoteViewModel>().ReverseMap();

        CreateMap<NoteCategory, NoteCategoryCreateModel>().ReverseMap();
        CreateMap<NoteCategory, NoteCategoryUpdateModel>().ReverseMap();
        CreateMap<NoteCategory, NoteCategoryViewModel>().ReverseMap();

        CreateMap<Habit, HabitCreateModel>().ReverseMap();
        CreateMap<Habit, HabitUpdateModel>().ReverseMap();
        CreateMap<Habit, HabitViewModel>().ReverseMap();

        CreateMap<Goal, GoalCreateModel>().ReverseMap();
        CreateMap<Goal, GoalUpdateModel>().ReverseMap();
        CreateMap<Goal, GoalViewModel>().ReverseMap();

        CreateMap<Event, EventCreateModel>().ReverseMap();
        CreateMap<Event, EventUpdateModel>().ReverseMap();
        CreateMap<Event, EventViewModel>().ReverseMap();


        CreateMap<Cashbook, CashbookCreateModel>().ReverseMap();
        CreateMap<Cashbook, CashbookUpdateModel>().ReverseMap();
        CreateMap<Cashbook, CashbookViewModel>().ReverseMap();
    }
}
