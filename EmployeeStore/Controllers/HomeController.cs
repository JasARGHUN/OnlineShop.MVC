using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EmployeeStore.Models.ViewModels;
using EmployeeStore.Models;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;
using Microsoft.AspNetCore.Routing;


namespace EmployeeStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmployeeRepository _context;
        public int pageSize = 6; // How many elements on 1 page
        //public int pages = 4; ReflectionIT.Mvc.Paging

        public HomeController(IEmployeeRepository context)
        {
            _context = context;
        }

        /*ReflectionIT.Mvc.Paging*/
        //public IActionResult Index(string filter, int page = 1, string sortExpression = "Name")
        //{
        //    var qry = _context.Employees.AsNoTracking().AsQueryable();
        //    var model = PagingList.Create(qry, pages, page, sortExpression, "Name");
        //    model.RouteValue = new RouteValueDictionary { { "filter", filter } };
        //    model.Action = "Index";
        //    return View(model);
        //}

        public async Task<IActionResult> Index(int? emp, string name, string category, int page = 1,
            SortState sortOrder = SortState.NameAsc)
        {
            IQueryable<Employee> source = _context.Employees.Where(p => category == null || p.Department == category)
                .OrderBy(p => p.Id);
            if (emp != null && emp != 0)
            {
                source = source.Where(p => p.Id == emp);
            }
            if (!String.IsNullOrEmpty(name))
            {
                source = source.Where(p => p.Name.Contains(name));
            }

            switch (sortOrder)
            {
                case SortState.NameDesc:
                    source = source.OrderByDescending(s => s.Name); break;
                case SortState.PriceAsc:
                    source = source.OrderBy(s => s.Price); break;
                case SortState.PriceDesc:
                    source = source.OrderByDescending(s => s.Price); break;
                case SortState.CategoryAsc:
                    source = source.OrderBy(s => s.Department); break;
                case SortState.CategoryDesc:
                    source = source.OrderByDescending(s => s.Department); break;
                default:
                    source = source.OrderBy(s => s.Name); break;
            }

            var count = await source.CountAsync();
            var items = await source.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            PagingInfo pagingInfo = new PagingInfo(count, page, pageSize);

            EmployeeListViewModel employeeListView = new EmployeeListViewModel
            {
                PagingInfo = pagingInfo,
                SortViewModel = new SortViewModel(sortOrder),
                FilterViewModel = new FilterViewModel(_context.Employees.ToList(), emp, name),
                Employees = items
            };

            return View(employeeListView);
        }

        public async Task<IActionResult> Details(int? id)
        {
            Employee model = await _context.GetEmployee(id.Value);

            if (model == null)
            {
                ViewBag.ErrorMessage = $"The user not found";
                return View("NotFound");
            }

            return View(model);
        }
    }
}
