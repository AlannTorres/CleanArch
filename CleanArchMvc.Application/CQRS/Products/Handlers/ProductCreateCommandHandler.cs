using CleanArchMvc.Application.CQRS.Products.Commands;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using MediatR;

namespace CleanArchMvc.Application.CQRS.Products.Handlers;

public class ProductCreateCommandHandler : IRequestHandler<ProductCreateCommand, Product>
{
    private readonly IProductRespository _productRespository;

    public ProductCreateCommandHandler(IProductRespository productRespository)
    {
        _productRespository = productRespository;
    }

    public async Task<Product> Handle(ProductCreateCommand request, CancellationToken cancellationToken)
    {
        var product = new Product(
            request.Name,
            request.Description,
            request.Price,
            request.Stock,
            request.Image
        );

        if (product == null) {

            throw new ApplicationException($"Error creating entity");
        }
        else
        {
            product.CategoryId = request.CategoryId;
            return await _productRespository.CreateAsync(product);
        }
    }
}
