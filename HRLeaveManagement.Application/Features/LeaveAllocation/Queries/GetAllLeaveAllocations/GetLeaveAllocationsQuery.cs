﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagement.Application.Features.LeaveAllocation.Queries.GetAllLeaveAllocations
{
    public record GetLeaveAllocationsQuery : IRequest<List<LeaveAllocationDto>>;
}