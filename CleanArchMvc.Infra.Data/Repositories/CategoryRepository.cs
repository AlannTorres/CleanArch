using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using CleanArchMvc.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace CleanArchMvc.Infra.Data.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly ApplicationDbContext _categoryContext;

    public CategoryRepository(ApplicationDbContext context)
    {
        _categoryContext = context;
    }

    public async Task<Category> GetByIdAsync(int id)
    {
#pragma warning disable CS8603 // Possível retorno de referência nula.
        return await _categoryContext.Categories.FindAsync(id);
#pragma warning restore CS8603 // Possível retorno de referência nula.
    }

    public async Task<IEnumerable<Category>> GetCategoriesAsync()
    {
        return await _categoryContext.Categories.ToListAsync();
    }

    public async Task<Category> CreateAsync(Category category)
    {
        _categoryContext.Add(category);
        await _categoryContext.SaveChangesAsync();

        return category;
    }

    public async Task<Category> RemoveAsync(Category category)
    {
        _categoryContext.Remove(category);
        await _categoryContext.SaveChangesAsync();
        return category;
    }

    public async Task<Category> UpdateAsync(Category category)
    {
        _categoryContext.Update(category);
        await _categoryContext.SaveChangesAsync();
        return category;
    }
}
