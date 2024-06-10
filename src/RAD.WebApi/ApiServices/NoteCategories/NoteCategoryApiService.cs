using AutoMapper;
using RAD.Domain.Entities;
using RAD.Services.Configurations;
using RAD.Services.Services.NoteCategories;
using RAD.WebApi.DTOs.NoteCategories;
using RAD.WebApi.Extensions;
using RAD.WebApi.Validators.NoteCategories;

namespace RAD.WebApi.ApiServices.NoteCategories;

public class NoteCategoryApiService(
    IMapper mapper,
    INoteCategoryService noteCategoryService,
    NoteCategoryCreateModelValidator createModelValidator,
    NoteCategoryUpdateModelValidator updateModelValidator) : INoteCategoryApiService
{
    public async ValueTask<bool> DeleteAsync(long id)
    {
        return await noteCategoryService.DeleteAsync(id);
    }

    public async ValueTask<NoteCategoryViewModel> GetAsync(long id)
    {
        var noteCategory = await noteCategoryService.GetByIdAsync(id);
        return mapper.Map<NoteCategoryViewModel>(noteCategory);
    }

    public async ValueTask<IEnumerable<NoteCategoryViewModel>> GetAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var noteCategories = await noteCategoryService.GetAllAsync(@params, filter, search);
        return mapper.Map<IEnumerable<NoteCategoryViewModel>>(noteCategories);
    }

    public async ValueTask<NoteCategoryViewModel> PostAsync(NoteCategoryCreateModel model)
    {
        await createModelValidator.EnsureValidatedAsync(model);
        var noteCategory = await noteCategoryService.CreateAsync(mapper.Map<NoteCategory>(model));
        return mapper.Map<NoteCategoryViewModel>(noteCategory);
    }

    public async ValueTask<NoteCategoryViewModel> PutAsync(long id, NoteCategoryUpdateModel model)
    {
        await updateModelValidator.EnsureValidatedAsync(model);
        var noteCategory = await noteCategoryService.UpdateAsync(id, mapper.Map<NoteCategory>(model));
        return mapper.Map<NoteCategoryViewModel>(noteCategory);
    }

    public async ValueTask<NoteCategoryViewModel> AddNoteToCategoryAsync(long categoryId, long noteId)
    {
        var noteCategory = await noteCategoryService.AddNoteToCategoryAsync(categoryId, noteId);
        return mapper.Map<NoteCategoryViewModel>(noteCategory);
    }

    public async ValueTask<NoteCategoryViewModel> RemoveNoteFromCategoryAsync(long categoryId, long noteId)
    {
        var noteCategory = await noteCategoryService.RemoveNoteFromCategoryAsync(categoryId, noteId);
        return mapper.Map<NoteCategoryViewModel>(noteCategory);
    }

    public async ValueTask<NoteCategoryViewModel> GetCategoryByNameAsync(string name)
    {
        var noteCategory = await noteCategoryService.GetCategoryByNameAsync(name);
        return mapper.Map<NoteCategoryViewModel>(noteCategory);
    }
}
