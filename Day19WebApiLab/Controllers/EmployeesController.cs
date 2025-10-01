using Day19WebApiLab.Data;
using Day19WebApiLab.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// Empty Web API project
namespace Day19WebApiLab.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // https://localhost:7045/api/Employees
    public class EmployeesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EmployeesController(AppDbContext context)
        {
            this._context = context;
        }
        //[HttpGet]
        //public IActionResult GetAll()
        //{
        //    var employees = _context.Employees.ToList();
        //    return Ok(employees);
        //}

        // By Async
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetAll()
        {
            var employees = await _context.Employees.ToListAsync();
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetById(int id)
        {
            //var employee = await _context.Employees.FindAsync(id);
            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Id == id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }
        [HttpPost]
        public async Task<IActionResult> Create(Employee employee)
        {
            if (employee == null)
            {
                return BadRequest(employee);
            }
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = employee.Id }, employee);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Employee employee)
        {
            if(employee == null || id != employee.Id)
            {
                return BadRequest();
            }
            var existingEmployee = await _context.Employees.FindAsync(id);
            if(existingEmployee == null)
            {
                return NotFound();
            }
            existingEmployee.Name = employee.Name;
            existingEmployee.Job = employee.Job;
            existingEmployee.Salary = employee.Salary;
            _context.Employees.Update(existingEmployee);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if(id <= 0)
            {
                return BadRequest();
            }
            var existingEmployee = await _context.Employees.FindAsync(id);
            if (existingEmployee == null)
            {
                return NotFound();
            }
            _context.Employees.Remove(existingEmployee);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
