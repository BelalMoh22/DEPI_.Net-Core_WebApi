using Day17WebApiCore9.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Day17WebApiCore9.Controllers
{
    // https://localhost:7281/api/departments
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        // Dependency Injection (DI) of the DbContext class
        private readonly Models.WebApiDBContext _DB;

        public DepartmentsController(Models.WebApiDBContext DB)
        {
            this._DB = DB;
        }
        // Get
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_DB.Departments.ToList());
        }

        // Get By ID
        [HttpGet("{id}")]
        public IActionResult GetByID(int id)
        {
            var department = _DB.Departments.Find(id);
            if (department == null)
            {
                return NotFound();
            }
            return Ok(department);
        }

        // Create
        [HttpPost]
        public IActionResult Create(Department department)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _DB.Departments.Add(department);
            _DB.SaveChanges();
            return Created();
        }
        // Edit
        [HttpPut]
        public IActionResult Update(int id, Department department)
        {
            if(id != department.DepartmentId)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest("ID Not Match");
            }else
            {
                var updatedDepartment = _DB.Departments.Find(id);
                if (updatedDepartment == null)
                {
                    return BadRequest(ModelState);
                }
                updatedDepartment.Name = department.Name;
                updatedDepartment.Description = department.Description;
                updatedDepartment.ManagerName = department.ManagerName;
                updatedDepartment.Location = department.Location;
                
                _DB.SaveChanges();
                return NoContent();
            }
        }
        // Delete
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        { 
            var deletedDepartment = _DB.Departments.Find(id);
            if(deletedDepartment == null)
            {
                return BadRequest(ModelState);
            }
            _DB.Departments.Remove(deletedDepartment);
            _DB.SaveChanges();
            return NoContent();
        }
    }
}
