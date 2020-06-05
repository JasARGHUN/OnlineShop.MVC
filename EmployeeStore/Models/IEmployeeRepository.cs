using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeStore.Models
{
    public interface IEmployeeRepository
    {
        IQueryable<Employee> Employees { get; }
        Task<Employee> GetEmployee(int? id); //need for update and delete employees.
        IEnumerable<Employee> GetAllEmployees(); //get all Employee data.
        Employee Add(Employee employee);
        Employee Update(Employee employeeDataUpdate);
        Task<Employee> Delete(int id);
    }
}
