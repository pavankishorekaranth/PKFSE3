using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Seller.API.Features.Commands.CreateProduct
{
    public class CreateProductCommand : IRequest<int>
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string ShortDescription { get; set; }
        public string DetailedDescription { get; set; }
        public decimal StartingPrice { get; set; }
        public DateTime BidEndDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int Pin { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}
