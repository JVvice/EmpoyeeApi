using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EmpoyeeApi.Models;
namespace EmpoyeeApi.Models
{
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string Email { get; set; }

        public string Phone { get; set; }

        public decimal Salary { get; set; }


        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        public Department Department { get; set; } //navigation property
    }
}
