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
        private readonly IDepartmentService _departmentService;
        public EmployeesContoller(IEmployeeService employeeService, IDepartmentService departmentService)
        {
            _employeeService = employeeService;
            _departmentService = departmentService;
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
            if (employee.Id != Guid.Empty)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            //add employee to employee table
            await _employeeService.AddEmployeeAsync(employee);

            //add employee to departments table
            await _departmentService.AddEmployeeToDepartment(employee);


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

            // Pass employee object to service to delete employee from department table
            await _departmentService.DeleteEmployeeFromDepartment(employee);

            return NoContent();

        }

    }
}
