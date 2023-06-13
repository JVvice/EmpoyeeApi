using EmpoyeeApi.Controllers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmpoyeeApi.Models
{
    public class Department
    {
        public Department()
        {
            Employees = new List<Employee>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DepartmentId { get; set; }

        [Required]
        public string DepartmentName { get; set; }

        [Required]
        public string Description { get; set; }

        public virtual List<Employee> Employees { get; set; }
    }
}
