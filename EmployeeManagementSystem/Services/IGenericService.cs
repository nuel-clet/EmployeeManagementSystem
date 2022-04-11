using EmployeeManagementSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Services
{
    public interface IGenericService<T>
    {
        Task<IEnumerable<T>> GetAllItemsAsync();      
        Task<T> GetItemByIdAsync(int id);
        Task CreateItemAsync(T entity);
        Task EditItemAsync(T entity);
        Task DeleteItemAsync(T entity);


        public EmployeeIndexViewModel GetEmployeAndDepartment();

        public EmployeeIndexViewModel GetEmployeAndDepartment(string searchString);

    }
}
