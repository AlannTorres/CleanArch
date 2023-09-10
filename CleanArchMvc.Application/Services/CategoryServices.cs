using AutoMapper;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Domain.Interfaces;

namespace CleanArchMvc.Application.Services;

public class CategoryServices : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public CategoryServices(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CategoryDTO>> GetCategories()
    {
        var categoriesEntity = await _categoryRepository.GetCategoriesAsync();
        return _mapper.Map<IEnumerable<CategoryDTO>>(categoriesEntity);
    }

    public async Task<CategoryDTO> GetById(int id)
    {
        var categoryEntity = await _categoryRepository.GetByIdAsync(id);
        return _mapper.Map<CategoryDTO>(categoryEntity);
    }

    public async Task Add(CategoryDTO categoryDTO)
    {
        throw new NotImplementedException();
    }

    public async Task Remove(int? id)
    {
        throw new NotImplementedException();
    }

    public async Task Update(CategoryDTO categoryDTO)
    {
        throw new NotImplementedException();
    }
}
