using EmployeeManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.ViewModels
{
    public class EmployeeIndexViewModel
    {
        public List<Employee> Employee { get; set; }
        public List<Department> Department { get; set; }
    }
}
