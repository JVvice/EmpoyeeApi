using EmpoyeeApi.Data;
using EmpoyeeApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmpoyeeApi.Controllers
{
    [ApiController]
    [Route("api/employees")]
    public class EmployeesContoller : ControllerBase
    {
        private AppDbContext _context;
        public EmployeesContoller(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            var employees = await _context.Employees.ToListAsync();
            return Ok(employees);
        }

    }
}
