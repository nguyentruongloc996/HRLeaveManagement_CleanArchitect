using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagement.Application.Features.LeaveType.Queries.GetLeaveTypesDetails
{
    public record GetLeaveTypesDetailsQuery(int Id) : IRequest<LeaveTypeDetailsDto>;
}
