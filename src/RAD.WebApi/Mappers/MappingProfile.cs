using AutoMapper;
using RAD.Domain.Entities;
using RAD.DTOs.Events;
using RAD.DTOs.Goals;
using RAD.DTOs.Habits;
using RAD.DTOs.Notes;
using RAD.DTOs.Tasks;
using RAD.DTOs.Users;
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
