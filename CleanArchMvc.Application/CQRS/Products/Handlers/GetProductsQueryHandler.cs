using CleanArchMvc.Application.CQRS.Products.Queries;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using MediatR;

namespace CleanArchMvc.Application.CQRS.Products.Handlers;

public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, IEnumerable<Product>>
{
    private readonly IProductRespository _productRespository;

    public GetProductsQueryHandler(IProductRespository productRespository)
    {
        _productRespository = productRespository ??
            throw new ArgumentNullException(nameof(productRespository));
    }

    public async Task<IEnumerable<Product>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        return await _productRespository.GetProductsAsync();
    }
}
