using Day20WebApiLab.Data;
using Day20WebApiLab.DTOs.EmployeeDTOs;
using Day20WebApiLab.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Day20WebApiLab.Controllers
{
    // https://localhost:7043/api/Employees
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EmployeesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetEmployeeDTO>>> Getemployees()
        {
            var employees = await _context.employees.ToListAsync();
            var departments = await _context.Departments.ToListAsync();

            var result = employees.Select(e => new GetEmployeeDTO
            {
                Id = e.Id,
                Name = e.Name,
                Job = e.Job,
                Salary = e.Salary,
                // Find the department name by joining with departments list
                departmentname = departments.FirstOrDefault(d => d.DepartmentId == e.departmentId)?.Name
            }).ToList();

            return Ok(result);
        }

        // https://localhost:7043/api/Employees?page=1&pageSize=10
        // https://localhost:7043/api/Employees/Pagination
        [HttpGet("Pagination")]
        public async Task<ActionResult<IEnumerable<GetEmployeeDTO>>> Getemployees([FromQuery] int page =1 , [FromQuery] int pageSize =5 )
        {
            var employees = await _context.employees.ToListAsync();
            var departments = await _context.Departments.ToListAsync();

            var result = employees.Select(e => new GetEmployeeDTO
            {
                Id = e.Id,
                Name = e.Name,
                Job = e.Job,
                Salary = e.Salary,
                // Find the department name by joining with departments list
                departmentname = departments.FirstOrDefault(d => d.DepartmentId == e.departmentId)?.Name
            }).ToList();

            //Modify For Pagination
            var totalCount = employees.Count();
            var TotalPage = (int)Math.Ceiling((double)totalCount / pageSize);
            result = result.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            return Ok(result);
        }

        // GET: api/Employees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetEmployeeDTO>> GetEmployee(int id)
        {
            if (id == 0 || id == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }
            var employee = await _context.employees.FindAsync(id);
            var departments = await _context.Departments.ToListAsync();

            if (employee == null)
            {
                return NotFound();
            }

            var foundEmployee = new GetEmployeeDTO()
            {
                Id = employee.Id,
                Name = employee.Name,
                Job = employee.Job,
                Salary = employee.Salary,
                departmentname = departments.FirstOrDefault(d => d.DepartmentId == employee.departmentId)?.Name
            };

            return Ok(foundEmployee);
        }

        // PUT: api/Employees/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee([FromRoute]int id, [FromBody]PutEmployeeDTO employee)
        {
            if (id != employee.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(employee);
            }

            var ExistingEmployee = await _context.employees.FindAsync(id);
            ExistingEmployee.Name = employee.Name;
            ExistingEmployee.Job = employee.Job;
            ExistingEmployee.Salary = employee.Salary;
            ExistingEmployee.departmentId = employee.departmentId;

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
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PostEmployeeDTO>> PostEmployee(PostEmployeeDTO employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var existingDepartment = await _context.Departments.FindAsync(employee.departmentId);
            if (existingDepartment == null)
            {
                return BadRequest($"Department with id {employee.departmentId} does not exist.");
            }

            var newEmployee = new Employee()
            {
                Name = employee.Name,
                Job = employee.Job,
                Salary = employee.Salary,
                departmentId = employee.departmentId
            };

            _context.employees.Add(newEmployee);
            await _context.SaveChangesAsync();
            var newID = _context.employees.Max(e => e.Id);
            return CreatedAtAction("GetEmployee", new { id = newID }, employee);

        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = await _context.employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            _context.employees.Remove(employee);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmployeeExists(int id)
        {
            return _context.employees.Any(e => e.Id == id);
        }
    }
}
