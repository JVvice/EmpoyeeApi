using EmpoyeeApi.Data;
using EmpoyeeApi.Interfaces;
using EmpoyeeApi.Migrations;
using EmpoyeeApi.Models;
using EmpoyeeApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmpoyeeApi.Controllers
{
    [ApiController]
    [Route("api/employees")]
    public class EmployeesContoller : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        public EmployeesContoller(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            var employees = await _employeeService.GetEmployeesAsync();
            return Ok(employees);
        }

        [HttpGet("{Id:Guid}", Name ="GetEmployee")]
        public async Task<ActionResult<Employee>> GetEmployeeByIdAsync(Guid id)
        {
            if(id == Guid.Empty)
            {
                return BadRequest();
            }

            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            
            if(employee == null) { return NotFound(); }

            return Ok(employee);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Employee), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Employee), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Employee), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Employee>>CreateEmployee([FromBody]Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (employee == null)
            {
                return BadRequest(employee);
            }
            if (employee.Id != Guid.Empty)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            await _employeeService.AddEmployeeAsync(employee);
            return CreatedAtRoute("GetEmployee", new { id = employee.Id }, employee);
        }

    }
}
