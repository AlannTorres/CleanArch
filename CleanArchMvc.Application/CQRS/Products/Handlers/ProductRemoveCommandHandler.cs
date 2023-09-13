using CleanArchMvc.Application.CQRS.Products.Commands;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using MediatR;

namespace CleanArchMvc.Application.CQRS.Products.Handlers;

public class ProductRemoveCommandHandler : IRequestHandler<ProductRemoveCommand, Product>
{
    private readonly IProductRespository _productRespository;

    public ProductRemoveCommandHandler(IProductRespository productRespository)
    {
        _productRespository = productRespository ??
            throw new ArgumentNullException(nameof(productRespository));
    }

    public async Task<Product> Handle(ProductRemoveCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRespository.GetByIdAsync(request.Id);

        if (product == null)
        {
            throw new ApplicationException($"Entity could not be found.");
        }
        else
        {
            var result = await _productRespository.RemoveAsync(product);
            return result;
        }
    }
}
