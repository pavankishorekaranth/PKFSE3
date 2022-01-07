using MediatR;

namespace Seller.Application.Commands
{
    public class DeleteProductCommand : IRequest
    {
        public string ProductId { get; set; }
    }
}
