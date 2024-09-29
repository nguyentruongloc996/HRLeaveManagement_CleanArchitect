using AutoMapper;
using HRLeaveManagement.Application.Features.LeaveAllocation.Queries.GetAllLeaveAllocations;
using HRLeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocationDetails;
using HRLeaveManagement.Application.Features.LeaveType.Commands;
using HRLeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType;
using HRLeaveManagement.Domain;

namespace HRLeaveManagement.Application.MappingProfiles
{
    internal class LeaveAllocationProfile : Profile
    {
        public LeaveAllocationProfile()
        {
            CreateMap<LeaveAllocationDto, LeaveAllocation>().ReverseMap();
            CreateMap<LeaveType, LeaveAllocationDetailsDto>();
            CreateMap<CreateLeaveTypeCommand, LeaveType>();
            CreateMap<UpdateLeaveTypeCommand, LeaveType>();
        }
    }
}
