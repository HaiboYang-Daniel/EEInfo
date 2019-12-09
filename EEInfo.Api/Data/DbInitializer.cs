using EEInfo.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EEInfo.Api.Data
{
    public static class DbInitializer
    {
        public static void Initialize(EEInfoDbContext context)
        {
            // Look for any employees.
            if (context.Employees.Any())
            {
                return;   // DB has been seeded
            }

            var employees = new Employee[]
            {
                new Employee { Id = Guid.NewGuid(), FirstName = "Carson",   LastName = "Alexander",
                    HiredDate = DateTime.Parse("2010-09-01") },
                new Employee { Id = Guid.NewGuid(), FirstName = "Meredith", LastName = "Alonso",
                    HiredDate = DateTime.Parse("2012-09-01") },
                new Employee { Id = Guid.NewGuid(), FirstName = "Arturo",   LastName = "Anand",
                    HiredDate = DateTime.Parse("2013-09-01") },
                new Employee { Id = Guid.NewGuid(), FirstName = "Gytis",    LastName = "Barzdukas",
                    HiredDate = DateTime.Parse("2012-09-01") },
                new Employee { Id = Guid.NewGuid(), FirstName = "Yan",      LastName = "Li",
                    HiredDate = DateTime.Parse("2012-09-01") },
                new Employee { Id = Guid.NewGuid(), FirstName = "Peggy",    LastName = "Justice",
                    HiredDate = DateTime.Parse("2011-09-01") },
                new Employee { Id = Guid.NewGuid(), FirstName = "Laura",    LastName = "Norman",
                    HiredDate = DateTime.Parse("2013-09-01") },
                new Employee { Id = Guid.NewGuid(), FirstName = "Nino",     LastName = "Olivetto",
                    HiredDate = DateTime.Parse("2005-09-01") }
            };

            foreach (Employee e in employees)
            {
                context.Employees.Add(e);
                var employeeTasks = new EmployeeTask[]
                {
                    new EmployeeTask { Id = Guid.NewGuid(), TaskName = "Task A", EmployeeId = e.Id,
                        StartTime = DateTime.Parse("2019-01-01"), Deadline = DateTime.Parse("2019-01-10") },
                    new EmployeeTask { Id = Guid.NewGuid(), TaskName = "Task B", EmployeeId = e.Id,
                        StartTime = DateTime.Parse("2019-02-01"), Deadline = DateTime.Parse("2019-02-10") },
                    new EmployeeTask { Id = Guid.NewGuid(), TaskName = "Task C", EmployeeId = e.Id,
                        StartTime = DateTime.Parse("2019-03-01"), Deadline = DateTime.Parse("2019-03-10") },
                    new EmployeeTask { Id = Guid.NewGuid(), TaskName = "Task D", EmployeeId = e.Id,
                        StartTime = DateTime.Parse("2019-04-01"), Deadline = DateTime.Parse("2019-04-10") },
                    new EmployeeTask { Id = Guid.NewGuid(), TaskName = "Task E", EmployeeId = e.Id,
                        StartTime = DateTime.Parse("2019-05-01"), Deadline = DateTime.Parse("2019-05-10") }
                };

                foreach (EmployeeTask t in employeeTasks)
                {
                    context.EmployeeTasks.Add(t);
                }
            }
            context.SaveChanges();
        }
    }
}
