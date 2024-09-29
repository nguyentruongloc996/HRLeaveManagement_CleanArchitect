using HRLeaveManagement.Domain;

namespace HRLeaveManagement.Application
{
    public interface ILeaveTypeRepository : IGenericRepository<LeaveType>
    {
        Task<bool> IsLeaveTypeUnique(string name);
    }
}
