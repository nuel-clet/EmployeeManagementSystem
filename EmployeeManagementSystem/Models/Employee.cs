using System;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementSystem.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
       
        public DateTime DateOfBirth { get; set; }

        public int DepartmentId { get; set; }
        public Department Department { get; set; }
    }
}
