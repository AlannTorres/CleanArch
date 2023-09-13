using AutoMapper;
using CleanArchMvc.Application.CQRS.Products.Commands;
using CleanArchMvc.Application.CQRS.Products.Queries;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using MediatR;

namespace CleanArchMvc.Application.Services;

public class ProductService : IProductService
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public ProductService(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }
    public async Task<ProductDTO> GetProductCategory(int id)
    {
        var productQuery = new GetProductByIdQuery(id);

        if (productQuery == null)
        {
            throw new ApplicationException($"Entity could not be loaded.");
        }

        var result = await _mediator.Send(productQuery);

        return _mapper.Map<ProductDTO>(result);
    }

    public async Task<IEnumerable<ProductDTO>> GetProducts()
    {
        var productsQuery = new GetProductsQuery();

        if (productsQuery == null)
        {
            throw new ApplicationException($"Entity could not be loaded.");
        }

        var result = await _mediator.Send(productsQuery);

        return _mapper.Map<IEnumerable<ProductDTO>>(result);
    }

    public async Task<ProductDTO> GetById(int id)
    {
        var productQuery = new GetProductByIdQuery(id);

        if (productQuery == null)
        {
            throw new ApplicationException($"Entity could not be loaded.");
        }

        var result = await _mediator.Send(productQuery);

        return _mapper.Map<ProductDTO>(result);
    }

    public async Task Add(ProductDTO productDTO)
    {
        var productCreateCommand = _mapper.Map<ProductCreateCommand>(productDTO);
        await _mediator.Send(productCreateCommand);
    }

    public async Task Update(ProductDTO productDTO)
    {
        var productUpdateCommand = _mapper.Map<ProductUpdateCommand>(productDTO);
        await _mediator.Send(productUpdateCommand);
    }

    public async Task Delete(int id)
    {
        var productRemoveCommand = new ProductRemoveCommand(id);

        if (productRemoveCommand == null)
        {
            throw new ApplicationException($"Entity could not be loaded.");
        }

        await _mediator.Send(productRemoveCommand);
    }
}
