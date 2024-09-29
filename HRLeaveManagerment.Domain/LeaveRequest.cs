using HRLeaveManagement.Domain.Common;

namespace HRLeaveManagement.Domain
{
    public class LeaveRequest : BaseEntity
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public LeaveType? LeaveType { get; set; }
        public int LeaveTypeID { get; set; }
        public DateTime DateRequestId { get; set; }
        public string? RequestComments { get; set; }
        public bool? Approved { get; set; }
        public bool Cancelled { get; set; }
        public string RequestingEmployeeId { get; set; } = string.Empty;
    }
}
