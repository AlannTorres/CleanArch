using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchMvc.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet()]
    public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategories()
    {
        var categories = await _categoryService.GetCategories();
        if (categories == null) return NotFound("Categories not found"); 
        return Ok(categories);
    }

    [HttpGet("{id:int}", Name = "GetCategoriesById")]
    public async Task<ActionResult<CategoryDTO>> GetCategoriesById(int id)
    {
        var category = await _categoryService.GetById(id);
        if (category == null) return NotFound("Category not found");
        return Ok(category);
    }

    [HttpPost()]
    public async Task<ActionResult> CreateCategory([FromBody] CategoryDTO categoryDTO)
    {
        if (categoryDTO == null) return BadRequest("Invalid data");
        await _categoryService.Add(categoryDTO);
        return new CreatedAtRouteResult("GetCategoriesById", new { id = categoryDTO.Id }, categoryDTO);
    }

    [HttpPut()]
    public async Task<ActionResult> EditCategory(int id, [FromBody] CategoryDTO categoryDTO)
    {
        if (id != categoryDTO.Id) return BadRequest("Id invalid");
        if (categoryDTO == null) return BadRequest("Invalid data");
        await _categoryService.Update(categoryDTO);
        return Ok(categoryDTO);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<CategoryDTO>> DeleteCategory(int id)
    {
        var category = await _categoryService.GetById(id);
        if (category == null) return NotFound("Category not found");
        await _categoryService.Remove(id);
        return Ok(category);
    }
}
