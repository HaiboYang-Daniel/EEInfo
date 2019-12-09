using System;

namespace EEInfo.Api.Models
{
    public class EmployeeTask
    {
        public Guid Id { get; set; }
        public Guid EmployeeId { get; set; }

        public string TaskName { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? Deadline { get; set; }
        public Employee Employee { get; set; }

    }
}
