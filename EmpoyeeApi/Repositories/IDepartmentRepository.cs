using System.Collections.Generic;
using System.Threading.Tasks;
using EmpoyeeApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmpoyeeApi.Repositories
{
    public interface IDepartmentRepository
    {
        Task<IEnumerable<Department>> GetDepartmentsAsync();
        Task<Department> AddDepartmentAsync(Department department);
    }
}