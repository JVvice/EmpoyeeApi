using EmpoyeeApi.Interfaces;
using EmpoyeeApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmpoyeeApi.Controllers
{
    [Route("api/department")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Department>>> GetDepartments()
        {
            var departments = await _departmentService.GetDepartmentsAsync();
            return Ok(departments);
        }

        [HttpGet("{id:Int}", Name = "GetDepartment")]
        public async Task<ActionResult<Department>> GetDepartment(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var department = await _departmentService.GetDepartmentAsync(id);

            if (department == null) { return NotFound(); }
            return Ok(department);
        }

        [HttpPost]
        public async Task<ActionResult<Department>> AddDeparment([FromBody]Department department)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if(department == null)
            {
                return BadRequest(department);
            }

            await _departmentService.AddDepartmentAsync(department);
            return Ok(department);

        }

        [HttpDelete("{id:Int}", Name ="DeleteDepartment")]
        public async Task<ActionResult> DeleteDepartment(int id)
        {
            if(id == 0)
            {
                return BadRequest();
            }
            var department = await _departmentService.DeleteDepartmentAsync(id);

            if(department == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }

}
