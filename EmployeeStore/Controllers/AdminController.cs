using EmployeeStore.Models;
using EmployeeStore.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;

namespace EmployeeStore.Controllers
{
    [Authorize(Roles = "admin")]
    [Authorize(Policy = "AdminRolePolicy")]
    public class AdminController : Controller
    {
        private readonly IEmployeeRepository _context;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public int pages = 2;

        public AdminController(IEmployeeRepository context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index(string filter, int page = 1, string sortExpression = "Name")
        {
            var qry = _context.Employees.AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter))
            {
                qry = qry.Where(d => d.Name.Contains(filter));
            }

            var model = PagingList.Create(qry, pages, page, sortExpression, "Name");
            model.RouteValue = new RouteValueDictionary { { "filter", filter } };
            model.Action = "Index";
            return View(model);
            //return View(_context.GetAllEmployees().ToList());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = await ProcessUploadFile(model);

                Employee newEmployee = new Employee
                {
                    Name = model.Name,
                    Email = model.Email,
                    Price = model.Price,
                    Department = model.Department,
                    Description = model.Description,
                    PhotoPath = uniqueFileName
                };
                _context.Add(newEmployee);

                TempData["message"] = $"Object {model.Name} was created.";
                return RedirectToAction("Index");
            }
            return View();
        }
        
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Employee emp = await _context.GetEmployee(id);

            EmployeeEditViewModel employeeEditViewModel = new EmployeeEditViewModel
            {
                Id = emp.Id,
                Name = emp.Name,
                Email = emp.Email,
                Price = emp.Price,
                Department = emp.Department,
                Description = emp.Description,
                ExistingPhotoPath = emp.PhotoPath
            };

            TempData["message"] = $"Object {employeeEditViewModel.Name} was selected.";
            return View(employeeEditViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EmployeeEditViewModel model)
        {

            if (ModelState.IsValid)
            {
                Employee employee = await _context.GetEmployee(model.Id);
                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.Price = model.Price;
                employee.Department = model.Department;
                employee.Description = model.Description;

                if (model.Photo != null)
                {
                    if (model.ExistingPhotoPath != null)
                    {
                        string filePath = Path.Combine(_hostingEnvironment.WebRootPath,
                            "images", model.ExistingPhotoPath);
                        System.IO.File.Delete(filePath);
                    }

                    employee.PhotoPath = await ProcessUploadFile(model);
                }

                _context.Update(employee);

                TempData["message"] = $"Object {employee.Name} was edited.";

                return RedirectToAction("index");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            Employee model = await _context.GetEmployee(id);

            if (model != null)
            {
                await _context.Delete(model.Id);

                TempData["message"] = $"Object {model.Name} was deleted.";

                return RedirectToAction("Index");
            }

            return View();
        }


        private async Task<string> ProcessUploadFile(EmployeeCreateViewModel model)
        {
            string uniqueFileName = null;

            if (model.Photo != null)
            {
                string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await model.Photo.CopyToAsync(fileStream);
                }
            }
            return uniqueFileName;
        }

        [HttpPost]
        public IActionResult SeedDatabase()
        {
            SeedDataBase.EnsurePopulated(HttpContext.RequestServices);
            return RedirectToAction(nameof(Index));
        }
    }
}
