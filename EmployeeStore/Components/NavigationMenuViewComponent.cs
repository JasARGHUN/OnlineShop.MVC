using EmployeeStore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace EmployeeStore.Components
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        private IEmployeeRepository _repository;

        public NavigationMenuViewComponent(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedCategory = RouteData?.Values["category"];
            return View(_repository.Employees
                .Select(x => x.Department)
                .Distinct()
                .OrderBy(x => x));
        }
    }
}
