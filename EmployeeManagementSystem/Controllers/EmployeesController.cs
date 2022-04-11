using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EmployeeManagementSystem.Data;
using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Services;
using Microsoft.AspNetCore.Authorization;

namespace EmployeeManagementSystem.Controllers
{
    [Authorize(Roles = "Admin")]
    public class EmployeesController : Controller
    {

        private readonly IGenericService<Employee> _genericService;

        public EmployeesController(IGenericService<Employee> genericService)
        {
            _genericService = genericService;
        }

        //[AllowAnonymous]
        //[HttpGet]
        //public IActionResult Index()
        //{
        //    var result = _genericService.GetEmployeAndDepartment();
        //    return View(result);
        //}

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Index(string searchString)
        {
            if (string.IsNullOrWhiteSpace(searchString))
            {
                var result = _genericService.GetEmployeAndDepartment();
                    return View(result);
            }

            ViewData["GetEmployeeDetails"] = searchString;

            var model = _genericService.GetEmployeAndDepartment(searchString);

            return View(model);
        }


        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {

            var employee = await _genericService.GetItemByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }      

            return View(employee);
        }
        
        [HttpGet]
        
        public IActionResult Create()
        {
            var department = _genericService.GetEmployeAndDepartment();
            ViewData["DepartmentId"] = new SelectList(department.Department, "Id", "Name");
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,DateOfBirth,DepartmentId")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                await _genericService.CreateItemAsync(employee);
                return RedirectToAction(nameof(Index));
            }
            var department = _genericService.GetEmployeAndDepartment();
            ViewData["DepartmentId"] = new SelectList(department.Department, "Id", "Name", employee.DepartmentId);
            return View(employee);
        }
        
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {

            var employee = await _genericService.GetItemByIdAsync(id);            
            if (employee == null)
            {
                return NotFound();
            }
            var department = _genericService.GetEmployeAndDepartment();
            ViewData["DepartmentId"] = new SelectList(department.Department, "Id", "Name", employee.DepartmentId);
            return View(employee);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,DateOfBirth,DepartmentId")] Employee employee)
        {
            if (id != employee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _genericService.EditItemAsync(employee);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (_genericService.GetItemByIdAsync(id) == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            var department = _genericService.GetEmployeAndDepartment();
            ViewData["DepartmentId"] = new SelectList(department.Department, "Id", "Name", employee.DepartmentId);
            return View(employee);
        }

        
        public async Task<IActionResult> Delete(int id)
        {
            var employee = await _genericService.GetItemByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _genericService.GetItemByIdAsync(id);
            await _genericService.DeleteItemAsync(employee);
            return RedirectToAction(nameof(Index));
        }

    }
}
