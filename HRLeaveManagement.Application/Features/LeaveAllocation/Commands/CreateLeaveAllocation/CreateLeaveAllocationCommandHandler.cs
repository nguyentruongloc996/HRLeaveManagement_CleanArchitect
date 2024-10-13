using AutoMapper;
using HRLeaveManagement.Application.Contracts.Logging;
using HRLeaveManagement.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation
{
    public class CreateLeaveAllocationCommandHandler : IRequestHandler<CreateLeaveAllocationCommand, Unit>
    {
        private ILeaveAllocationRepository leaveAllocationRepository;
        private ILeaveTypeRepository leaveTypeRepository;
        private IMapper mapper;
        private IAppLogger<CreateLeaveAllocationCommandHandler> logger;
        public CreateLeaveAllocationCommandHandler(ILeaveAllocationRepository leaveAllocationRepository, 
            IMapper mapper, 
            IAppLogger<CreateLeaveAllocationCommandHandler> appLogger, 
            ILeaveTypeRepository leaveTypeRepository)
        {
            this.leaveAllocationRepository = leaveAllocationRepository;
            this.mapper = mapper;
            this.logger = appLogger;
            this.leaveTypeRepository = leaveTypeRepository;
        }

        public async Task<Unit> Handle(CreateLeaveAllocationCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateLeaveAllocationCommandValidator(leaveTypeRepository);
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if (validatorResult.Errors.Any())
            {
                throw new BadRequestException("Invalid Leave Allocation Request",
                    validatorResult);
            }

            // Get leave type for Allocations
            var leaveType = await leaveTypeRepository.GetByIdAsync(request.LeaveTypeId);

            // Get Employees

            // Get Period

            // Assign Allocation
            var leaveAllocation = mapper.Map<Domain.LeaveAllocation>(request);
            await leaveAllocationRepository.CreateAsync(leaveAllocation);

            return Unit.Value;
        }
    }
}
