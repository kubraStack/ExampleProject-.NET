using Core.CrossCuttingConcerns.Exceptions.Types;
using DataAccess.Abstracts;
using Entities;
using MediatR;


namespace Business.Features.Products.Commands.Delete
{
    public class DeleteProductCommand : IRequest
    {
        public int Id { get; set; }

        public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
        {
            private readonly IProductRepository _productRepository;

            public DeleteProductCommandHandler(IProductRepository productRepository)
            {
                _productRepository = productRepository;
            }

            public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
            {
                Product? product = await _productRepository.GetAsync(p => p.Id == request.Id);
                if (product is null)
                    throw new BusinessExeption("Böyle bir veri bulunamadı.");

                await _productRepository.DeleteAsync(product);
            }
        }
    }
}
