using AutoMapper;
using RAD_BackEnd.Domain.Entities;
using RAD_BackEnd.DTOs.Notes;
using RAD_BackEnd.Services.Configurations;
using RAD_BackEnd.Services.Services.Notes;
using RAD_BackEnd.WebApi.Extensions;
using RAD_BackEnd.WebApi.Validators.Notes;

namespace RAD_BackEnd.WebApi.ApiServices.Notes;

public class NoteApiService(
    IMapper mapper,
    INoteService noteService,
    NoteCreateModelValidator createModelValidator,
    NoteUpdateModelValidator updateModelValidator) : INoteApiService
{
    public async ValueTask<bool> DeleteAsync(long id)
    {
        return await noteService.DeleteAsync(id);
    }

    public async ValueTask<NoteViewModel> GetAsync(long id)
    {
        var note = await noteService.GetByIdAsync(id);
        return mapper.Map<NoteViewModel>(note);
    }

    public async ValueTask<IEnumerable<NoteViewModel>> GetAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var notes = await noteService.GetAllAsync(@params, filter, search);
        return mapper.Map<IEnumerable<NoteViewModel>>(notes);
    }

    public async ValueTask<NoteViewModel> PostAsync(NoteCreateModel model)
    {
        await createModelValidator.EnsureValidatedAsync(model);
        var note = await noteService.CreateAsync(mapper.Map<Note>(model));
        return mapper.Map<NoteViewModel>(note);
    }

    public async ValueTask<NoteViewModel> PutAsync(long id, NoteUpdateModel model)
    {
        await updateModelValidator.EnsureValidatedAsync(model);
        var note = await noteService.UpdateAsync(id, mapper.Map<Note>(model));
        return mapper.Map<NoteViewModel>(note);
    }
}
