using EmployeeManagementSystem.Data;
using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Services
{
    public class GenericService<T> : IGenericService<T> where T : class
    {
        private AppDbContext _context;
        protected DbSet<T> Dbset;
        public GenericService(AppDbContext context)
        {
            _context = context;
            Dbset = context.Set<T>();
        }


        public async Task CreateItemAsync(T entity)
        {
            Dbset.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteItemAsync(T entity)
        {
            Dbset.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task EditItemAsync(T entity)
        {
            Dbset.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllItemsAsync()
        {
           return await Dbset.ToArrayAsync();
        }
        
        public async Task<T> GetItemByIdAsync(int id)
        {          
            return await Dbset.FindAsync(id);
        }



        public EmployeeIndexViewModel GetEmployeAndDepartment()
        {


            var employees = _context.Employees
                           .Include(dept => dept.Department)
                           .ToList();
            var departments = _context.Departments.ToList();

            var models = new EmployeeIndexViewModel
            {
                Employee = employees,
                Department = departments
            };
            return models;
        }
        public EmployeeIndexViewModel GetEmployeAndDepartment(string searchString)
        {

            DateTime date;

            if (DateTime.TryParse(searchString, out date))
            {
                var employees = _context.Employees
                              .Include(dept => dept.Department)
                              .Where(x =>
                                 x.DateOfBirth.Date == date)
                              .ToList();
                var departments = _context.Departments.ToList();

                var model = new EmployeeIndexViewModel
                {
                    Employee = employees,
                    Department = departments
                };
                return model;
            }

            else
            {
                var employees = _context.Employees
                .Include(dept => dept.Department)
                .Where(x =>
                x.FirstName.Contains(searchString)
                || x.LastName.Contains(searchString)
                || x.Department.Name.Contains(searchString))
                .ToList();

                var departments = _context.Departments.ToList();

                var model = new EmployeeIndexViewModel
                {
                    Employee = employees,
                    Department = departments
                };
                return model;
            }
        }

    }
}
