using Day20WebApiLab.Data;
using Day20WebApiLab.DTOs.DepartmentDTOs;
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
    // https://localhost:7043/api/Departments
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DepartmentsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Departments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetDepartmentDTO>>> Getdepartments()
        {
            var departments = await _context.Departments.ToListAsync();
            // Mapping from Departments to DepartmentDTO
            List<GetDepartmentDTO> result = new List<GetDepartmentDTO>();
            foreach (var item in departments)
            {
                result.Add(new GetDepartmentDTO
                {
                    DepartmentId = item.DepartmentId,
                    Description = item.Description,
                    Name = item.Name
                });
            }
            return Ok(result);
        }

        // https://localhost:7043/api/Departments?page=1&pageSize=10
        // Pagination
        [HttpGet("Pagination")]
        public async Task<ActionResult<IEnumerable<GetDepartmentDTO>>> GetdepartmentsPagination([FromQuery]int page = 1 , [FromQuery]int pageSize= 10)
        {
            var departments = await _context.Departments.ToListAsync();
            // Mapping from Departments to DepartmentDTO
            List<GetDepartmentDTO> result = new List<GetDepartmentDTO>();
            foreach (var item in departments)
            {
                result.Add(new GetDepartmentDTO
                {
                    DepartmentId = item.DepartmentId,
                    Description = item.Description,
                    Name = item.Name
                });
            }
            //Modify For Pagination
            var totalCount = departments.Count();
            var TotalPage = (int) Math.Ceiling((double)totalCount / pageSize);
            result = result.Skip( (page - 1) * pageSize).Take(pageSize).ToList();
            return Ok(result);
        }

        // GET: api/Departments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetDepartmentDTO>> GetDepartment(int id)
        {
            if (id == 0 || id == null)
            {
                return BadRequest();
            }

            var department = await _context.Departments.FindAsync(id);
            if (department == null)
            {
                return NotFound();
            }

            GetDepartmentDTO foundDepartment = new GetDepartmentDTO()
            {
                DepartmentId = department.DepartmentId,
                Description = department.Description,
                Name = department.Name
            };

            return Ok(foundDepartment);
        }

        // PUT: api/Departments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        // https://localhost:7043/api/Departments/9
        public async Task<IActionResult> PutDepartment([FromRoute]int id, [FromBody]PutDepartmentDTO department)
        {
            if (id != department.DepartmentId)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid) 
            {
                return BadRequest(department);
            }
            //_context.Entry(department).State = EntityState.Modified;
            var ExistingDepartment = await _context.Departments.FindAsync(id);
            ExistingDepartment.Name = department.Name;
            ExistingDepartment.Description = department.Description;
            ExistingDepartment.ManagerName = department.ManagerName;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DepartmentExists(id))
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

        // POST: api/Departments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GetDepartmentDTO>> PostDepartment(PostDepartmentDTO department)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            _context.Departments.Add(
                new Department
                {
                    Name = department.Name,
                    Description = department.Description,
                    ManagerName = department.ManagerName
                }    
            );
            await _context.SaveChangesAsync();

            //Location 
            var newID = _context.Departments.Max(d => d.DepartmentId);
            //return Created();
            //return CreatedAtAction("GetDepartment", new { id = department.DepartmentId }, department);
            return CreatedAtAction("GetDepartment", new { id = newID }, department);
        }

        // DELETE: api/Departments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            if(id == 0 || id == null)
            {
                return BadRequest();
            }
            var department = await _context.Departments.FindAsync(id);
            if (department == null)
            {
                return NotFound();
            }

            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DepartmentExists(int id)
        {
            return _context.Departments.Any(e => e.DepartmentId == id);
        }
    }
}
