using HRLeaveManagement.Domain;

namespace HRLeaveManagement.Application
{
    public interface ILeaveRequestRepository : IGenericRepository<LeaveRequest>
    {
        public Task<List<LeaveRequest>> GetLeaveRequestWithDetails(int id);
        public Task<List<LeaveRequest>> GetLeaveRequestWithDetails();
        public Task<List<LeaveRequest>> GetLeaveRequestWithDetails(string userId);
    }
}
