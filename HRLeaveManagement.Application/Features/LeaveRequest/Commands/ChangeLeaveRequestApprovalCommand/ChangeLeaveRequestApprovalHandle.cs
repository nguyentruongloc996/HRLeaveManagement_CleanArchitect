using AutoMapper;
using FluentValidation;
using HRLeaveManagement.Application.Contracts.Email;
using HRLeaveManagement.Application.Exceptions;
using HRLeaveManagement.Application.Models.Email;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagement.Application.Features.LeaveRequest.Commands.ChangeLeaveRequestApprovalCommand
{
    public class ChangeLeaveRequestApprovalHandle : IRequestHandler<ChangeLeaveRequestApprovalCommand, Unit>
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IEmailSender _emailSender;
        private readonly IMapper _mapper;

        public ChangeLeaveRequestApprovalHandle (ILeaveRequestRepository leaveRequestRepository,
            ILeaveTypeRepository leaveTypeRepository,
            IEmailSender emailSender,
            IMapper mapper
            )
        {
            _leaveRequestRepository = leaveRequestRepository;
            _leaveTypeRepository = leaveTypeRepository;
            _emailSender = emailSender;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(ChangeLeaveRequestApprovalCommand request, CancellationToken cancellationToken)
        {
            // check if Leave request exists
            var leaveRequest = await _leaveRequestRepository.GetByIdAsync(request.Id);

            if (leaveRequest == null)
            {
                throw new NotFoundException(nameof(leaveRequest), request.Id);
            }

            var validator = new ChangeLeaveRequestApprovalCommandValidator();
            var validateResult = await validator.ValidateAsync(request);

            if(validateResult.Errors.Any())
            {
                // Todo: do something.
            }

            // change Approve status
            leaveRequest.Approved = request.Approved;
            await _leaveRequestRepository.UpdateAsync(leaveRequest);

            // send email
            var email = new EmailMessage
            {
                To = string.Empty,
                Body = $"The approval status for your leave request for {leaveRequest.StartDate:D} to {leaveRequest.EndDate:D} " +
                "has been approved.",
                Subject = "Leave Request Approval Status Updated"
            };

            await _emailSender.SendEmail(email);
            return Unit.Value;
        }
    }
}
