using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EEInfo.Api.Data;
using EEInfo.Api.Models;

namespace EEInfo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly EEInfoDbContext _context;

        public EmployeesController(EEInfoDbContext context)
        {
            _context = context;
        }

        // GET: api/Employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            return await _context.Employees.ToListAsync();
        }

        // GET: api/Employees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(Guid id)
        {
            var employee = await _context.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            if (employee.EmployeeTasks == null)
            {
                employee.EmployeeTasks = new List<EmployeeTask>();
            }

            return employee;
        }
        
        // GET: api/Employees/5
        [HttpGet("{id}/EmployeeTasks")]
        public async Task<ActionResult<IEnumerable<EmployeeTask>>> GetEmployeeTasks(Guid id)
        {
            var employeeTasks = await _context.EmployeeTasks.Where(x => x.EmployeeId == id)
                .OrderBy(x => x.StartTime).ToListAsync();

            if (employeeTasks == null)
            {
                return NotFound();
            }

            return employeeTasks;
        }

        // PUT: api/Employees/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(Guid id, Employee employee)
        {
            if (id != employee.Id)
            {
                return BadRequest();
            }

            _context.Entry(employee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Employees
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
        {
            employee.Id = Guid.NewGuid();
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmployee", new { id = employee.Id }, employee);
        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Employee>> DeleteEmployee(Guid id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            _context.EmployeeTasks.RemoveRange(
                _context.EmployeeTasks.Where(x => x.EmployeeId == id));

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            employee.EmployeeTasks = null;

            return employee;
        }

        private bool EmployeeExists(Guid id)
        {
            return _context.Employees.Any(e => e.Id == id);
        }
    }
}
