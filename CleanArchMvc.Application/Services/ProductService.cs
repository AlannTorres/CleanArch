using AutoMapper;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;

namespace CleanArchMvc.Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRespository _productRespository;
    private readonly IMapper _mapper;

    public ProductService(IProductRespository productRespository, IMapper mapper)
    {
        _productRespository = productRespository;
        _mapper = mapper;
    }
    public async Task<ProductDTO> GetProductCategory(int id)
    {
        var productEntity = await _productRespository.GetProductCategoryAsync(id);
        return _mapper.Map<ProductDTO>(productEntity);
    }

    public async Task<IEnumerable<ProductDTO>> GetProducts()
    {
        var productsEntity = await _productRespository.GetProductsAsync();
        return _mapper.Map<IEnumerable<ProductDTO>>(productsEntity);
    }

    public async Task<ProductDTO> GetById(int id)
    {
        var productEntity = await _productRespository.GetByIdAsync(id);
        return _mapper.Map<ProductDTO>(productEntity);
    }

    public async Task Add(ProductDTO productDTO)
    {
        var productEntity = _mapper.Map<Product>(productDTO);
        await _productRespository.CreateAsync(productEntity);
    }

    public async Task Update(ProductDTO productDTO)
    {
        var productEntity = _mapper.Map<Product>(productDTO);
        await _productRespository.UpdateAsync(productEntity);
    }

    public async Task Delete(int id)
    {
        var productEntity = _productRespository.GetByIdAsync(id).Result;
        await _productRespository.RemoveAsync(productEntity);
    }
}
