using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;

namespace CleanArchMvc.WebUI.Controllers;

public class ProductsController : Controller
{
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;
    private readonly IWebHostEnvironment _environment;

    public ProductsController(IProductService productService, ICategoryService categoryService, IWebHostEnvironment environment)
    {
        _productService = productService;
        _categoryService = categoryService;
        _environment = environment;
    }

    [HttpGet]
    public async Task<IActionResult> IndexProducts()
    {
        var products = await _productService.GetProducts();
        return View(products);
    }

    [HttpGet()]
    public async Task<IActionResult> CreateProduct()
    {
        ViewBag.CategoryId = new SelectList(await _categoryService.GetCategories(), "Id", "Name");
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct(ProductDTO productDTO)
    {
 
        if (ModelState.IsValid)
        {
            await _productService.Add(productDTO);
            return RedirectToAction(nameof(IndexProducts));
        }
        else
        {
            ViewBag.CategoryId = new SelectList(await _categoryService.GetCategories(), "Id", "Name");
        }
        return View(productDTO);
    }

    [HttpGet]
    public async Task<IActionResult> EditProduct(int id)
    {
        if (id.Equals(null)) NotFound();

        var productDTO = await _productService.GetById(id);

        if (productDTO == null) NotFound();

        var categories = await _categoryService.GetCategories();
        ViewBag.CategoryId = new SelectList(categories, "Id", "Name", productDTO.CategoryId);

        return View(productDTO);
    }

    [HttpPost]
    public async Task<IActionResult> EditProduct(ProductDTO productDTO)
    {
        if (ModelState.IsValid)
        {
            try { await _productService.Update(productDTO); }
            catch { throw; }
            return RedirectToAction(nameof(IndexProducts));
        }
        return View(productDTO);
    }

    [HttpGet]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        if (id.Equals(null)) NotFound();

        var productDTO = await _productService.GetById(id);

        if (productDTO == null) NotFound();

        return View(productDTO);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteConfirmedProduct(int id)
    {
        await _productService.Delete(id);
        return RedirectToAction(nameof(IndexProducts));
    }

    public async Task<IActionResult> DetailsProduct(int id)
    {
        if (id.Equals(null)) { return NotFound(); }
        var productDTO = await _productService.GetById(id);

        if (productDTO.Equals(null)) { return NotFound(); }
        var wwwroot = _environment.WebRootPath;
        var image = Path.Combine(wwwroot, "images\\" + productDTO.Image);
        var exists = System.IO.File.Exists(image);
        ViewBag.ImageExist = exists;

        return View(productDTO);
    }
}
