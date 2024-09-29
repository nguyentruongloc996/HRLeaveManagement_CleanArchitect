using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation
{
    public class CreateLeaveAllocationCommandValidator : AbstractValidator<CreateLeaveAllocationCommand>
    {
        private readonly ILeaveAllocationRepository leaveAllocationRepository;

        public CreateLeaveAllocationCommandValidator(ILeaveAllocationRepository leaveAllocationRepository)
        {
            this.leaveAllocationRepository = leaveAllocationRepository;
        }
    }
}
