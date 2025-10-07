using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Day21WebApiLab.Data;
using Day21WebApiLab.Models;
using Day21WebApiLab.DTOs.EmployeeDTO;

namespace Day21WebApiLab.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EmployeesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Employees
        [HttpGet]
        [Consumes("application/json")]
        [Produces("application/json")]
        // https://localhost:7007/api/Employees
        public async Task<ActionResult<IEnumerable<GetEmployeeDTO>>> Getemployees()
        {
            var emps = await _context.employees.Include("department").AsNoTracking().ToListAsync();
            List <GetEmployeeDTO> result = new List<GetEmployeeDTO>();
            foreach (var item in emps) {
                result.Add(
                    new GetEmployeeDTO()
                    {
                        Name = item.Name,
                        Job = item.Job,
                        Salary = item.Salary.ToString("C"),
                        DepartmentName = item.department.Name,
                        ManagerName = item.department.ManagerName
                    }
                );
            
            }
            return Ok(result);
        }

        // GET: api/Employees/5
        // https://localhost:7007/api/Employees/1
        [HttpGet("{id}")]
        public async Task<ActionResult<GetEmployeeDTO>> GetEmployee(int id)
        {
            var employee = await _context.employees.Include("department").AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);

            if (employee == null)
            {
                return NotFound();
            }

            var ExistingEmployee = new GetEmployeeDTO()
            { 
                Name = employee.Name,
                Job = employee.Job,
                Salary = employee.Salary.ToString("C"),
                DepartmentName = employee.department.Name,
                ManagerName = employee.department.ManagerName
            };

            return ExistingEmployee;
        }

        // Pagination
        // https://localhost:7007/api/Employees?page=1&pageSize=10
        [HttpGet("Pagination")]
        public async Task<ActionResult<IEnumerable<GetEmployeeDTO>>> GetEmployeePagination([FromQuery] int page = 1, [FromQuery] int pageSize = 3)
        {
            var employees = await _context.employees.Include("department").AsNoTracking().ToListAsync();
            // Mapping from Departments to DepartmentDTO
            List<GetEmployeeDTO> result = new List<GetEmployeeDTO>();
            foreach (var item in employees)
            {
                result.Add(new GetEmployeeDTO
                {
                    Name = item.Name,
                    Job = item.Job,
                    Salary = item.Salary.ToString("C"),
                    DepartmentName = item.department.Name,
                    ManagerName = item.department.ManagerName
                });
            }
            //Modify For Pagination
            var totalCount = employees.Count();
            var TotalPage = (int)Math.Ceiling((double)totalCount / pageSize);
            result = result.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            return Ok(result);
        }

        // PUT: api/Employees/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee([FromRoute]int id, [FromBody]PostPutEmployeeDTO employee)
        {
            if (id != employee.Id)
            {
                return BadRequest();
            }

            var existingDepartment = await _context.Departments.FindAsync(employee.departmentId);
            if (existingDepartment == null)
            {
                return BadRequest($"Department with id {employee.departmentId} does not exist.");
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
        //[Consumes("application/json")]
        [Consumes("text/xml")]
        [Produces("application/json")]
        public async Task<ActionResult<Employee>> PostEmployee(PostPutEmployeeDTO employee)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(employee);
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
                departmentId = employee.departmentId,
            };

            _context.employees.Add(newEmployee);

            await _context.SaveChangesAsync();

            int newID = _context.employees.Max(e => e.Id);
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
