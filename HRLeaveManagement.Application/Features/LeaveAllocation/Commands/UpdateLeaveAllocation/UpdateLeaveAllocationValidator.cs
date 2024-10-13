using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagement.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation
{
    public class UpdateLeaveAllocationValidator : AbstractValidator<UpdateLeaveAllocationCommand>
    {
        private readonly ILeaveTypeRepository leaveTypeRepository;
        private readonly ILeaveAllocationRepository leaveAllocationRepository;

        public UpdateLeaveAllocationValidator(ILeaveTypeRepository leaveTypeRepository, ILeaveAllocationRepository leaveAllocationRepository)
        {
            this.leaveTypeRepository = leaveTypeRepository;
            this.leaveAllocationRepository = leaveAllocationRepository;

            RuleFor(p => p.NumberOfDays)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must greater than {ComparisonValue}");

            RuleFor(p => p.Period)
                .GreaterThanOrEqualTo(DateTime.Now.Year)
                .WithMessage("{PropertyName} must be after {ComparisonValue}");

            RuleFor(p => p.LeaveTypeId)
                .GreaterThan(0)
                .MustAsync(LeaveTypeMustExist)
                .WithMessage("{PropertyName} must be present");

            RuleFor(p => p.Id)
                .NotNull()
                .MustAsync(LeaveAllocationMustExist)
                .WithMessage("{PropertyName} must be present");
        }

        private async Task<bool> LeaveAllocationMustExist(int leaveAllocationId, CancellationToken token)
        {
            var leaveAllocation = await leaveAllocationRepository.GetByIdAsync(leaveAllocationId);
            return leaveAllocation != null;
        }

        private async Task<bool> LeaveTypeMustExist(int leaveTypeId, CancellationToken token)
        {
            var leaveType = await leaveTypeRepository.GetByIdAsync(leaveTypeId);
            return leaveType != null;
        }
    }
}
