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

        public async Task InsertEmployee(EmployeeDto employee)
        {
            var password = new PasswordHasher<EmployeeDto>();
            var hashed = password.HashPassword(employee, employee.Password);

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
                SecurityStamp = Guid.NewGuid().ToString("D"),
                PasswordHash = hashed
            };


            await _db.Employees.AddAsync(newEmployee);
            await _db.SaveChangesAsync();
        }

        public async Task<EmployeesTotal> GetEmployees(string sort, string order, int page, int size, string search)
        {
            IQueryable<Employee> employeesQuery = _db.Employees.OrderBy(s => s.Id);

            switch (sort)
            {
                case "FirstName":
                    if (order == "desc")
                        employeesQuery = _db.Employees.OrderByDescending(s => s.FirstName);
                    else
                        employeesQuery = _db.Employees.OrderBy(s => s.FirstName);
                    break;
                case "LastName":
                    if (order == "desc")
                        employeesQuery = _db.Employees.OrderByDescending(s => s.LastName);
                    else
                        employeesQuery = _db.Employees.OrderBy(s => s.LastName);
                    break;
                case "Email":
                    if (order == "desc")
                        employeesQuery = _db.Employees.OrderByDescending(s => s.Email);
                    else
                        employeesQuery = _db.Employees.OrderBy(s => s.Email);
                    break;
                case "PhoneNumber":
                    if (order == "desc")
                        employeesQuery = _db.Employees.OrderByDescending(s => s.PhoneNumber);
                    else
                        employeesQuery = _db.Employees.OrderBy(s => s.PhoneNumber);
                    break;
            }

            List<Employee> employees;
            int total;

            if (search != null && search.Length > 2)
            {
                employees = await employeesQuery
                    .Where(s => s.Active == true)
                    .Where(s => s.FirstName.Contains(search) || s.LastName.Contains(search))
                    .Skip(page * size)
                    .Take(size)
                    .ToListAsync();
                total = await employeesQuery
                    .Where(s => s.Active == true)
                    .Where(s => s.FirstName.Contains(search) || s.LastName.Contains(search))
                    .CountAsync();
            }
            else
            {
                employees = await employeesQuery.Where(s => s.Active == true)
                    .Skip(page * size)
                    .Take(size)
                    .ToListAsync();
                total = await employeesQuery.Where(s => s.Active == true).CountAsync();
            }

            EmployeesTotal employeesTotal = new EmployeesTotal
            {
                Employees = employees,
                TotalEmployees = total
            };

            return employeesTotal;
        }

        public async Task<Employee> GetEmployee(string id)
        {
            return await _db.Employees.Where(s => s.Active == true && s.Id == id).FirstAsync();
        }

        public async Task<bool> EditEmployee(EmployeeDto employee)
        {
            var targetEmployee = await _db.Employees.Where(s => s.Active == true && s.Email == employee.Email).FirstAsync();

            if (targetEmployee != null)
            {
                var password = new PasswordHasher<EmployeeDto>();
                var hashed = password.HashPassword(employee, employee.Password);

                targetEmployee.FirstName = employee.FirstName;
                targetEmployee.LastName = employee.LastName;
                targetEmployee.Email = employee.Email;
                targetEmployee.PasswordHash = hashed;
                targetEmployee.PhoneNumber = employee.PhoneNumber;

                await _db.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
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
