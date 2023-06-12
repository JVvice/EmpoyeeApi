using EmpoyeeApi.Data;
using EmpoyeeApi.Interfaces;
using EmpoyeeApi.Migrations;
using EmpoyeeApi.Models;
using EmpoyeeApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;

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
            if(employee.Id == Guid.Empty)
            {
                employee.Id = Guid.NewGuid();
            }
            else
            {
                return BadRequest();
            }

            //add employee to employee table
            await _employeeService.AddEmployeeAsync(employee);


            return CreatedAtRoute("GetEmployee", new { id = employee.Id }, employee);
        }

        [HttpDelete]
        public async Task<ActionResult>DeleteEmployee(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }
            //get employee using id
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            if (employee == null) { return NotFound(); };

            //delete employee from employee table
            var deletedEmp = await _employeeService.DeleteEmployeeAsync(id);

            if(deletedEmp == null)
            {
                return NotFound();
            }

            return NoContent();

        }

        [HttpPut("{id:Guid}", Name = "UpdateEmployee")]
        public IActionResult UpdateEmployee(Guid id, [FromBody]Employee employee)
        {
            if(employee == null || id != employee.Id)
            {
                return BadRequest();
            }

            this._employeeService.UpdateEmployeeAsync(employee);
            return NoContent();


        }

    }
}
