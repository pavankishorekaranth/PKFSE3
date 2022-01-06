﻿using AutoMapper;
using Buyer.Application.Contracts.Persistence;
using Buyer.Application.Queries;
using Buyer.Application.ViewModel;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Buyer.Application.Handlers
{
    public class GetBidQueryHandler : IRequestHandler<GetBidQuery, BidInfo>
    {
        private readonly IMapper _mapper;
        private readonly IBidRepository _bidRepository;
        private readonly ILogger<GetBidQueryHandler> _logger;

        public GetBidQueryHandler(IMapper mapper, IBidRepository bidRepository, ILogger<GetBidQueryHandler> logger)
        {
            _mapper = mapper;
            _bidRepository = bidRepository;
            _logger = logger;
        }

        public async Task<BidInfo> Handle(GetBidQuery request, CancellationToken cancellationToken)
        {
            var bid = await _bidRepository.GetBidById(request.Id);
            if (bid == null)
            {
                return null;
            }
            return _mapper.Map<BidInfo>(bid);
        }
    }
}
