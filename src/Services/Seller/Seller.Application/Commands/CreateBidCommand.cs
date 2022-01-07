﻿using MediatR;

namespace Seller.Application.Commands
{
    public class CreateBidCommand : IRequest<string>
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int Pin { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public decimal BidAmount { get; set; }
        public string ProductId { get; set; }
    }
}
