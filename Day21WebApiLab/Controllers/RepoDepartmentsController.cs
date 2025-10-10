using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Day21WebApiLab.Data;
using Day21WebApiLab.Models;
using Day21WebApiLab.Repositories;

namespace Day21WebApiLab.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RepoDepartmentsController : ControllerBase
    {
        private readonly IDepartmentRepository _repo;

        public RepoDepartmentsController(IDepartmentRepository repo)
        {
            _repo = repo;
            //_repo = new DepartmentRepository();
        }

        // GET: api/RepoDepartments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Department>>> GetDepartments()
        {
            return Ok(_repo.GetAll());
        }

        // GET: api/RepoDepartments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Department>> GetDepartment(int id)
        {
            var department = _repo.GetByID(id);

            if (department == null)
            {
                return NotFound();
            }

            return department;
        }

        // PUT: api/RepoDepartments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDepartment(int id, Department department)
        {
            if (id != department.DepartmentId)
            {
                return BadRequest();
            }

            _repo.Update(department);
            _repo.Save();
            return NoContent();
        }

        // POST: api/RepoDepartments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Department>> PostDepartment(Department department)
        {
            _repo.Add(department);
            _repo.Save();
            return CreatedAtAction("GetDepartment", new { id = department.DepartmentId }, department);
        }

        // DELETE: api/RepoDepartments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            _repo.Delete(id);
            _repo.Save();
            return NoContent();
        }

    }
}
