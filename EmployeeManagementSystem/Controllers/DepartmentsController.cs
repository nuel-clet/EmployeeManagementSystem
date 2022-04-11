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
    public class DepartmentsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IGenericService<Department> _genericService;

        public DepartmentsController(AppDbContext context,
                                    IGenericService<Department> genericService)
        {
            _context = context;
            _genericService = genericService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var result = await _genericService.GetAllItemsAsync();
            return View(result);
        }
        
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {            
            var department = await _genericService.GetItemByIdAsync(id);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }
        
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Department department)
        {
            if (ModelState.IsValid)
            {
                await _genericService.CreateItemAsync(department);
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }
        
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {            

            var department = await _genericService.GetItemByIdAsync(id);
            if (department == null)
            {
                return NotFound();
            }
            return View(department);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Department department)
        {
            if (id != department.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _genericService.EditItemAsync(department);                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (_genericService.GetItemByIdAsync(department.Id) == null)
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
            return View(department);
        }

        
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var department = await _genericService.GetItemByIdAsync(id);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var department = await _genericService.GetItemByIdAsync(id);
            await _genericService.DeleteItemAsync(department);
            return RedirectToAction(nameof(Index));
        }

    }
}
