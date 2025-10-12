using FullArcheticture_WebApiLab.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FullArcheticture_WebApiLab.DTOs.DepartmentDtos;

namespace FullArcheticture_WebApiLab.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RepoDepartmentsController : ControllerBase
    {
        private readonly IServiceDepartment _service;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="service"></param>
        public RepoDepartmentsController(IServiceDepartment service)
        {
            this._service = service;
        }

        /// <summary>
        ///  This EndPoint To Get All Departments
        /// </summary>
        /// <returns> List Of Department</returns>
        /// <example>
        /// GET : api/Departments
        /// </example>
        [HttpGet]
        [Produces("application/json")]

        public async Task<ActionResult<IEnumerable<GetDepartmentDto>>> GetDepartments()
        {
            return Ok(_service.GetDepartments());
        }

        /// <summary>
        /// This EndPoint To Get One Department By Id 
        /// </summary>
        /// <param name="id"> Int Number </param>
        /// <returns> One Department Details</returns>
        /// <remarks>
        ///   For Request: api/Departments/5
        /// </remarks>
        // GET: api/Departments/5
        [HttpGet("{id}")]
        [ProducesResponseType<GetDepartmentDto>(200)]
        [ProducesResponseType<GetDepartmentDto>(404, Type = (typeof(void)))]
        public async Task<ActionResult<GetDepartmentDto>> GetDepartment(int id)
        {
            var result = _service.GetDepartmentByID(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        /// <summary>
        /// This EndPoint To Update the Department 
        /// </summary>
        /// <param name="id"> int Number for ID</param>
        /// <param name="department">New Department Details</param>
        /// <returns>NoContent</returns>
        // PUT: api/Departments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Produces("application/json")] // Response
        [Consumes("application/json")] // Sending (Request)
        [ProducesResponseType(204, Type = (typeof(void)))]
        [ProducesResponseType(400, Type = (typeof(void)))]
        public async Task<IActionResult> PutDepartment(int id, PutDepartmentDto department)
        {
            if (id != department.DepartmentId)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid) return BadRequest(department);
            try
            {
                _service.UpdateDepartment(department);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return NoContent(); //204
        }

        // POST: api/Departments
        /// <summary>
        /// This EndPoint Creates a new Department
        /// </summary>
        /// <param name="department">New Department</param>
        /// <returns> Created </returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Consumes("application/json")]
        [Produces("Application/Json")]
        public async Task<ActionResult<PostDepartmentDto>> PostDepartment(PostDepartmentDto department)
        {
            if (!ModelState.IsValid) return BadRequest(department);
            _service.AddDepartment(department);
            int newId = _service.GetMaxID();

            return CreatedAtAction("GetDepartment", new { id = newId }, department);
        }
        // DELETE: api/Departments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            var department = _service.GetDepartmentByID(id);
            if (department == null)
            {
                return NotFound();
            }
            _service.DeleteDepartment(id);
            return NoContent();
        }
        // GET: api/Departments
        /// <summary>
        /// This EndPoint For Get List of Departments With Pagination 
        /// </summary>
        /// <param name="page">Current Page Integer Number default 1</param>
        /// <param name="pageSize">Page Size of row default 10</param>
        /// <returns>List of Department</returns>
        [HttpGet("Pagination")]
        public async Task<ActionResult<IEnumerable<GetDepartmentDto>>> GetWithPagination([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var result = _service.PaginationDepartments(page, pageSize);
            return Ok(result);
        }
        [HttpGet("EmployeeNames")]
        public async Task<ActionResult<IEnumerable<GetDepartmentWithEmpNamesDto>>> GetDepartmentEmpNames()
        {
            var result = _service.DepartmentWithEmployee("Employees");
            return Ok(result);
        }
    }
}
