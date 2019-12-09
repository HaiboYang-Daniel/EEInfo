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
    public class EmployeeTasksController : ControllerBase
    {
        private readonly EEInfoDbContext _context;

        public EmployeeTasksController(EEInfoDbContext context)
        {
            _context = context;
        }

        // GET: api/EmployeeTasks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeTask>>> GetEmployeeTasks()
        {
            return await _context.EmployeeTasks.ToListAsync();
        }

        // GET: api/EmployeeTasks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeTask>> GetEmployeeTask(Guid id)
        {
            var employeeTask = await _context.EmployeeTasks.FindAsync(id);

            if (employeeTask == null)
            {
                return NotFound();
            }

            return employeeTask;
        }

        // PUT: api/EmployeeTasks/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployeeTask(Guid id, EmployeeTask employeeTask)
        {
            if (id != employeeTask.Id)
            {
                return BadRequest();
            }

            _context.Entry(employeeTask).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeTaskExists(id))
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

        // POST: api/EmployeeTasks
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<EmployeeTask>> PostEmployeeTask(EmployeeTask employeeTask)
        {
            employeeTask.Id = Guid.NewGuid();
            _context.EmployeeTasks.Add(employeeTask);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmployeeTask", new { id = employeeTask.Id }, employeeTask);
        }

        // DELETE: api/EmployeeTasks/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<EmployeeTask>> DeleteEmployeeTask(Guid id)
        {
            var employeeTask = await _context.EmployeeTasks.FindAsync(id);
            if (employeeTask == null)
            {
                return NotFound();
            }

            _context.EmployeeTasks.Remove(employeeTask);
            await _context.SaveChangesAsync();

            return employeeTask;
        }

        private bool EmployeeTaskExists(Guid id)
        {
            return _context.EmployeeTasks.Any(e => e.Id == id);
        }
    }
}
