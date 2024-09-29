using AutoMapper;
using HRLeaveManagement.Application.Contracts.Logging;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagement.Application.Features.LeaveAllocation.Queries.GetAllLeaveAllocations
{
    public class GetLeaveAllocationsQueryHandle : IRequestHandler<GetLeaveAllocationsQuery, List<LeaveAllocationDto>>
    {
        private readonly ILeaveAllocationRepository leaveAllocationRepository;
        private readonly IMapper mapper;
        private IAppLogger<GetLeaveAllocationsQueryHandle> logger;

        public GetLeaveAllocationsQueryHandle(ILeaveAllocationRepository leaveAllocationRepository, IMapper mapper, IAppLogger<GetLeaveAllocationsQueryHandle> logger)
        {
            this.leaveAllocationRepository = leaveAllocationRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<List<LeaveAllocationDto>> Handle(GetLeaveAllocationsQuery request, CancellationToken cancellationToken)
        {
            // Query in DB.
            var leaveAllocations = await leaveAllocationRepository.GetAsync();
            // Convert data object to Dto object.
            var data = mapper.Map<List<LeaveAllocationDto>>(leaveAllocations);

            // Return the list.
            logger.LogInformation("Leave Allocations were retrived successfullly");
            return data;
        }
    }
}
