using System;
using System.Collections.Generic;

namespace EEInfo.Api.Models
{
    public class Employee
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? HiredDate { get; set; }
        public ICollection<EmployeeTask> EmployeeTasks { get; set; }
    }
}
