﻿using HRLeaveManagement.Domain;

namespace HRLeaveManagement.Application
{
    public interface ILeaveAllocationRepository : IGenericRepository<LeaveAllocation>
    {
        Task<LeaveAllocation?> GetLeaveAllocationWithDetails(int id);
        Task<List<LeaveAllocation>> GetLeaveAllocationWithDetails();
        Task<List<LeaveAllocation>> GetLeaveAllocationWithDetails(string userId);
        Task<bool> AllocationExists(string userId, int leaveTypeId, int period);
        Task AddAllocation(List<LeaveAllocation> allocations);
        Task<LeaveAllocation?> GetUserAllocations(string userId, int leaveTypeId);
    }
}