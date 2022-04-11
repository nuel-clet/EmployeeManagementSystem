using System.Collections.Generic;

namespace EmployeeManagementSystem.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual IEnumerable<Employee> Employees { get; set; }

    }
}
