using AutoMapper;
using HRLeaveManagement.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HRLeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType
{
    public class CreateLeaveTypeCommandHandler : IRequestHandler<CreateLeaveTypeCommand, int>
    {
        private readonly IMapper mapper;
        private readonly ILeaveTypeRepository leaveTypeRepository;

        public CreateLeaveTypeCommandHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository)
        {
            this.mapper = mapper;
            this.leaveTypeRepository = leaveTypeRepository;
        }

        async Task<int> IRequestHandler<CreateLeaveTypeCommand, int>.Handle(CreateLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            // Validate incomming data
            var validator = new CreateLeaveTypeCommandValidator(leaveTypeRepository);
            var validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid) {
                throw new BadRequestException("Invalid Leavetype", validationResult);
            }

            // convert to domain entity object
            var leaveTypeToCreate = mapper.Map<Domain.LeaveType>(request);

            // add to database
            await leaveTypeRepository.CreateAsync(leaveTypeToCreate);

            // return record id

            return leaveTypeToCreate.Id;
        }
    }
}
