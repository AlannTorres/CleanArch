using CleanArchMvc.Application.CQRS.Products.Queries;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using MediatR;

namespace CleanArchMvc.Application.CQRS.Products.Handlers;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Product>
{
    private readonly IProductRespository _productRespository;

    public GetProductByIdQueryHandler(IProductRespository productRespository)
    {
        _productRespository = productRespository ??
            throw new ArgumentNullException(nameof(productRespository));
    }

    public async Task<Product> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        return await _productRespository.GetByIdAsync(request.Id);
    }
}
