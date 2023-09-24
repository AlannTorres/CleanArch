using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchMvc.WebUI.Controllers;

[Authorize]
public class CategoriesController : Controller
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<IActionResult> IndexCategories()
    {
        var categories = await _categoryService.GetCategories();
        return View(categories);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> CreateCategory()
    {
        return View();
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> CreateCategory(CategoryDTO categoryDTO)
    {
        if (ModelState.IsValid)
        {
            await _categoryService.Add(categoryDTO);
            return RedirectToAction(nameof(IndexCategories));
        }
        return View(categoryDTO);
    }

    [HttpGet]
    public async Task<IActionResult> DetailsCategory(int id)
    {
        if (id.Equals(null)) NotFound();

        var categoryDTO = await _categoryService.GetById(id);

        if (categoryDTO == null) NotFound();

        return View(categoryDTO);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> EditCategory(int id)
    {
        if (id.Equals(null)) NotFound();

        var categoryDTO = await _categoryService.GetById(id);

        if (categoryDTO == null) NotFound();

        return View(categoryDTO);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> EditCategory(CategoryDTO categoryDTO)
    {
        if (ModelState.IsValid)
        {
            try { await _categoryService.Update(categoryDTO); }
            catch { throw; }
            return RedirectToAction(nameof(IndexCategories));
        }
        return View(categoryDTO);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        if (id.Equals(null)) NotFound();

        var categoryDTO = await _categoryService.GetById(id);

        if (categoryDTO == null) NotFound();

        return View(categoryDTO);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> DeleteConfirmedCategory(int id)
    {
        await _categoryService.Remove(id);
        return RedirectToAction(nameof(IndexCategories));
    }
}
