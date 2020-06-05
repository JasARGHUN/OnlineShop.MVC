using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EmployeeStore.Models.ViewModels
{
    public class FilterViewModel
    {
        public FilterViewModel(List<Employee> employees, int? employee, string name)
        {
            employees.Insert(0, new Employee { Name = "All", Id = 0 });
            Objects = new SelectList(employees, "Id", "Name", employee);
            SelectedObject = employee;
            SelectedName = name;
        }
        public SelectList Objects { get; private set; } 
        public int? SelectedObject { get; private set; } 
        public string SelectedName { get; private set; } 
        public string SelectedCategory { get; private set; } 
    }
}
