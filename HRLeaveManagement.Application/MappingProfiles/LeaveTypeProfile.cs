using AutoMapper;
using HRLeaveManagement.Application.Features.LeaveType.Commands;
using HRLeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType;
using HRLeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using HRLeaveManagement.Application.Features.LeaveType.Queries.GetLeaveTypesDetails;
using HRLeaveManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagement.Application.MappingProfiles
{
    internal class LeaveTypeProfile : Profile
    {
        public LeaveTypeProfile()
        {
            CreateMap<LeaveTypeDto, LeaveType>().ReverseMap();
            CreateMap<LeaveType,LeaveTypeDetailsDto>();
            CreateMap<CreateLeaveTypeCommand,LeaveType>();
            CreateMap<UpdateLeaveTypeCommand,LeaveType>();
        }
    }
}
