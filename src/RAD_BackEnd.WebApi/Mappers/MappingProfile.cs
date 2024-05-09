using AutoMapper;
using RAD_BackEnd.Domain.Entities;
using RAD_BackEnd.DTOs.Events;
using RAD_BackEnd.DTOs.Goals;
using RAD_BackEnd.DTOs.Habits;
using RAD_BackEnd.DTOs.Notes;
using RAD_BackEnd.DTOs.Tasks;
using RAD_BackEnd.DTOs.Users;
using Task = RAD_BackEnd.Domain.Entities.Task;

namespace RAD_BackEnd.WebApi.Mappers;

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

        CreateMap<Note, NoteCreateModel>().ReverseMap();
        CreateMap<Note, NoteUpdateModel>().ReverseMap();
        CreateMap<Note, NoteViewModel>().ReverseMap();

        CreateMap<Habit, HabitCreateModel>().ReverseMap();
        CreateMap<Habit, HabitUpdateModel>().ReverseMap();
        CreateMap<Habit, HabitViewModel>().ReverseMap();

        CreateMap<Goal, GoalCreateModel>().ReverseMap();
        CreateMap<Goal, GoalUpdateModel>().ReverseMap();
        CreateMap<Goal, GoalViewModel>().ReverseMap();

        CreateMap<Event, EventCreateModel>().ReverseMap();
        CreateMap<Event, EventUpdateModel>().ReverseMap();
        CreateMap<Event, EventViewModel>().ReverseMap();
    }
}
