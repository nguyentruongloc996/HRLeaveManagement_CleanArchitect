using AutoMapper;
using HRLeaveManagement.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagement.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation
{
    public class UpdateLeaveAllocationCommandHandle : IRequestHandler<UpdateLeaveAllocationCommand, Unit>
    {
        private IMapper mapper;
        private ILeaveTypeRepository leaveTypeRepository;
        private ILeaveAllocationRepository leaveAllocationRepository;

        public UpdateLeaveAllocationCommandHandle(ILeaveTypeRepository leaveTypeRepository,
            ILeaveAllocationRepository leaveAllocationRepository,
            IMapper mapper)
        {
            this.leaveTypeRepository = leaveTypeRepository;
            this.leaveAllocationRepository = leaveAllocationRepository;
            this.mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateLeaveAllocationCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateLeaveAllocationValidator(leaveTypeRepository, leaveAllocationRepository);
            var validatorResult = await validator.ValidateAsync(request);

            if (validatorResult.Errors.Any())
            {
                throw new BadRequestException("Invalid Leave Allocation", validatorResult);
            }

            var leaveAllocation = await leaveAllocationRepository.GetByIdAsync(request.Id);
            if (leaveAllocation == null)
            {
                throw new NotFoundException(nameof(LeaveAllocation), request.Id);
            }

            mapper.Map(request, leaveAllocation);
            await leaveAllocationRepository.UpdateAsync(leaveAllocation);

            return Unit.Value;
        }
    }
}
