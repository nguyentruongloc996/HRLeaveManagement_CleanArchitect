using AutoMapper;
using HRLeaveManagement.Application.Contracts.Email;
using HRLeaveManagement.Application.Exceptions;
using HRLeaveManagement.Application.Models.Email;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagement.Application.Features.LeaveRequest.Commands.CreateLeaveRequest
{
    public class CreateLeaveRequestCommandHandle : IRequestHandler<CreateLeaveRequestCommand, Unit>
    {
        private ILeaveTypeRepository _leaveTypeRepository;
        private ILeaveRequestRepository _leaveRequestRepository;
        private IMapper _mapper;
        private IEmailSender _emailSender;

        public CreateLeaveRequestCommandHandle(ILeaveTypeRepository leaveTypeRepository, 
            ILeaveRequestRepository leaveRequestRepository, 
            IMapper mapper, 
            IEmailSender emailSender)
        {
            _leaveTypeRepository = leaveTypeRepository;
            _leaveRequestRepository = leaveRequestRepository;
            _mapper = mapper;
            _emailSender = emailSender;
        }

        public async Task<Unit> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateLeaveRequestCommandValidator(_leaveTypeRepository);
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (validationResult.Errors.Any())
            {
                throw new BadRequestException("Invalid Leave Request", validationResult);
            }

            // Get requesting employee's id

            // Check on employee's allocation

            // If allocations aren't enough, return validation error with message

            // Create leave request
            var leaveRequest = _mapper.Map<Domain.LeaveRequest>(request);
            await _leaveRequestRepository.CreateAsync(leaveRequest);

            // send confirmation email
            var email = new EmailMessage
            {
                To = string.Empty,
                Body = $"Your leave request for {request.StartDate:D} to {request.EndDate:D} " +
                    $"has been submitted succefully.",
                Subject = "Leave Request Summitted"
            };

            await _emailSender.SendEmail(email);

            return Unit.Value;
        }
    }
}
