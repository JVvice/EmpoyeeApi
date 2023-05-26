using System.ComponentModel.DataAnnotations;

namespace EmpoyeeApi.Models
{
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string Description { get; set; }
    }
}
