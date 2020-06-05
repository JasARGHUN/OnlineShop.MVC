using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeStore.Models
{
    public class EFEmployeeRepository : IEmployeeRepository
    {
        private ApplicationDbContext _context;

        public EFEmployeeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<Employee> Employees => _context.Employees;

        public async Task<Employee> GetEmployee(int? id) //Метод получает обьект из БД.
        {
            return await _context.Employees.FindAsync(id);
        }
        public Employee Add(Employee employee)
        {
            _context.Employees.Add(employee);
            _context.SaveChanges();
            return employee;
        }

        public async Task<Employee> Delete(int id)
        {
            Employee emp = await _context.Employees.FindAsync(id);
            if (emp != null)
            {
                _context.Employees.Remove(emp);
                _context.SaveChanges();
            }
            return emp;
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return _context.Employees;
        }

        public Employee Update(Employee employeeDataUpdate)
        {
            var emp = _context.Employees.Attach(employeeDataUpdate);
            emp.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return employeeDataUpdate;
        }
    }
}
