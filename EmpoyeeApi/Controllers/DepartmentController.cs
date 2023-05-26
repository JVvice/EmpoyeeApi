using EmpoyeeApi.Models;
using EmpoyeeApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EmpoyeeApi.Controllers
{
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        [HttpGet]
        [Route("api/departments")]
        public async Task<ActionResult<IEnumerable<Department>>> GetDepartments()
        {
            var departments = await _departmentRepository.GetDepartmentsAsync();
            return Ok(departments);
        }

        [HttpPost]
        [Route("api/departments")]
        public async Task<ActionResult<Department>> AddDeparment([FromBody]Department department)
        {
            await _departmentRepository.AddDepartmentAsync(department);
            return Ok();

        }
    }

}
