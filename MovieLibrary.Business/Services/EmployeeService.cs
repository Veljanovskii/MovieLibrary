using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MovieLibrary.Data.DataModels;
using MovieLibrary.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLibrary.Business.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly MovielibraryContext _db;

        public EmployeeService(MovielibraryContext db)
        {
            _db = db;
        }

        public async Task InsertEmployee(Employee employee)
        {
            var password = new PasswordHasher<Employee>();
            var hashed = password.HashPassword(employee, employee.PasswordHash);
            employee.PasswordHash = hashed;

            Employee newEmployee = new Employee
            {
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Active = true,
                UserName = employee.Email,
                NormalizedUserName = employee.Email.ToUpper(),
                Email = employee.Email,
                NormalizedEmail = employee.Email,
                EmailConfirmed = true,
                PhoneNumber = employee.PhoneNumber,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D")                
            };

            await _db.Employees.AddAsync(newEmployee);
            await _db.SaveChangesAsync();
        }

        public Task<EmployeesTotal> GetEmployees(string sort, string order, int page, int size, string search)
        {
            throw new NotImplementedException();
        }

        public async Task<Employee> GetEmployee(string id)
        {
            return await _db.Employees.Where(s => s.Active == true && s.Id == id).FirstAsync();
        }

        public Task<bool> EditEmployee(Employee employee)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteEmployee(string id)
        {
            var targetEmployee = await _db.Employees.FindAsync(id);

            if (targetEmployee != null)
            {
                targetEmployee.Active = false;

                await _db.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
