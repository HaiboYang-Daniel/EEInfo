using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EEInfo.Models
{
    public class Employee
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [DataType(DataType.Date)]
        public DateTime? HiredDate { get; set; }
        public ICollection<EmployeeTask> EmployeeTasks { get; set; }
    }
}
