using AutoMapper;
using HRLeaveManagement.Application.Contracts.Logging;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagement.Application.Features.LeaveRequest.Queries.GetAllLeaveRequests
{
    public class GetLeaveRequestsQueryHandle : IRequestHandler<GetLeaveRequestsQuery, List<LeaveRequestDto>>
    {
        private readonly ILeaveRequestRepository leaveRequestRepository;
        private readonly IMapper mapper;
        private readonly IAppLogger<GetLeaveRequestsQueryHandle> logger;

        public GetLeaveRequestsQueryHandle(ILeaveRequestRepository leaveRequestRepository, IMapper mapper, IAppLogger<GetLeaveRequestsQueryHandle> logger)
        {
            this.leaveRequestRepository = leaveRequestRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<List<LeaveRequestDto>> Handle(GetLeaveRequestsQuery request, CancellationToken cancellationToken)
        {
            // TODO: Check if it is logged in employee

            var leaveRequests = await leaveRequestRepository.GetAsync();
            var data = mapper.Map<List<LeaveRequestDto>>(leaveRequests);

            // Fill requests with employee details

            logger.LogInformation("Leave Requests were retrived successfully");
            return data;
        }
    }
}
