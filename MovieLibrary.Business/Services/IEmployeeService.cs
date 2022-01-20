using MovieLibrary.Data.DataModels;
using MovieLibrary.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLibrary.Business.Services
{
    public interface IEmployeeService
    {
        public Task InsertEmployee(Employee employee);
        public Task<EmployeesTotal> GetEmployees(string sort, string order, int page, int size, string search);
        public Task<Employee> GetEmployee(string id);
        public Task<bool> EditEmployee(Employee employee);
        public Task<bool> DeleteEmployee(string id);
    }
}
