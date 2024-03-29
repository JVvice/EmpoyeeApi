﻿using System.Collections.Generic;
using System.Threading.Tasks;
using EmpoyeeApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmpoyeeApi.Interfaces
{
    public interface IDepartmentService
    {
        Task<List<Department>> GetDepartmentsAsync();
        Task<Department> GetDepartmentAsync(int id);
        Task<Department> AddDepartmentAsync(Department department);
        Task<Department> DeleteDepartmentAsync(int id);

    }
}