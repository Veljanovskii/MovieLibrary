using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using MovieLibrary.Data.Models;
using System.Globalization;

namespace MovieLibrary.Business.Services
{
    public class SeedDataService : ISeedDataService
    {
        private readonly MovielibraryContext _db;
        private readonly UserManager<Employee> _userManager;

        public SeedDataService(MovielibraryContext db, UserManager<Employee> userManager)
        {
            _db = db;
            _userManager = userManager;
        }
        public void Initialize()
        {
            CreateMaritalStatuses();
            CreateMovies();
            CreateCustomers();

            CreateRoles();
            CreateEmployee();

            AssignRoles("jack.doe@example.com", "Administrator").Wait();

            _db.SaveChangesAsync();
        }

        internal void CreateMaritalStatuses()
        {
            if (!_db.MaritalStatuses.Any())
            {
                List<MaritalStatus> list = new List<MaritalStatus>()
                {
                    new MaritalStatus { MaritalStatusId = 1, Caption = "Single" },
                    new MaritalStatus { MaritalStatusId = 2, Caption = "Married" },
                    new MaritalStatus { MaritalStatusId = 3, Caption = "Divorced" },
                    new MaritalStatus { MaritalStatusId = 4, Caption = "Widowed" },
                };

                _db.MaritalStatuses.AddRange(list);

                _db.SaveChanges();
            }
        }

        internal void CreateMovies()
        {
            if (!_db.Movies.Any())
            {
                List<Movie> movies = new List<Movie>()
                {
                    new Movie
                    {
                        Caption = "The Wolf of Wallstreet",
                        ReleaseYear = 2013,
                        MovieLength = 180,
                        InsertDate = DateTime.ParseExact("2021-12-21 15:50:35.703", "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture),
                        DeleteDate = null
                    },
                    new Movie
                    {
                        Caption = "Pulp Fiction",
                        ReleaseYear = 1994,
                        MovieLength = 140,
                        InsertDate = DateTime.ParseExact("2021-12-22 11:43:11.777", "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture),
                        DeleteDate = null
                    },
                    new Movie
                    {
                        Caption = "Avatar",
                        ReleaseYear = 2009,
                        MovieLength = 170,
                        InsertDate = DateTime.ParseExact("2021-12-22 16:14:25.437", "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture),
                        DeleteDate = null
                    }
                };

                _db.Movies.AddRange(movies);

                _db.SaveChanges();
            }
        }

        internal void CreateCustomers()
        {
            if (!_db.Customers.Any())
            {
                List<User> customers = new List<User>()
                {
                    new User
                    {
                        FirstName = "John",
                        LastName = "Doe",
                        Address = "123 Main St",
                        Idnumber = "123456789",
                        MaritalStatusId = 1,
                        InsertDate = DateTime.ParseExact("2021-12-22 18:30:28.537", "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture),
                        DeleteDate = null
                    },
                    new User
                    {
                        FirstName = "Jane",
                        LastName = "Doe",
                        Address = "321 5th St",
                        Idnumber = "987654321",
                        MaritalStatusId = 3,
                        InsertDate = DateTime.ParseExact("2021-12-22 18:40:11.610", "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture),
                        DeleteDate = null
                    }
                };

                _db.Customers.AddRange(customers);

                _db.SaveChanges();
            }
        }

        internal void CreateRoles()
        {
            //var roleStore = new RoleStore<IdentityRole>(_db);

            //if (!_db.Roles.Any())
            //{
            //    roleStore.CreateAsync(new IdentityRole() { Name = "Administrator", NormalizedName = "Administrator".ToUpper() });
            //    roleStore.CreateAsync(new IdentityRole() { Name = "User", NormalizedName = "User".ToUpper() });
            //}

            if (!_db.Roles.Any()) 
            {
                List<IdentityRole> roles = new List<IdentityRole>()
                {
                    new IdentityRole() { Name = "Administrator", NormalizedName = "Administrator".ToUpper() },
                    new IdentityRole() { Name = "User", NormalizedName = "User".ToUpper() }
                };

                _db.Roles.AddRange(roles);
                _db.SaveChanges();
            }
        }

        internal void CreateEmployee()
        {
            if(!_db.Employees.Any())
            {
                var employee = new Employee
                {
                    FirstName = "Jack",
                    LastName = "Doe",
                    Active = true,
                    Email = "jack.doe@example.com",
                    NormalizedEmail = "JACK.DOE@EXAMPLE.COM",
                    UserName = "jack.doe@example.com",
                    NormalizedUserName = "JACK.DOE@EXAMPLE.COM",
                    PhoneNumber = "+111111111111",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString("D")
                };

                var password = new PasswordHasher<Employee>();
                var hashed = password.HashPassword(employee, "secret");
                employee.PasswordHash = hashed;

                _db.Employees.Add(employee);
                _db.SaveChanges();
            }
        }

        internal async Task<IdentityResult> AssignRoles(string email, string role)
        {
            Employee employee = await _userManager.FindByEmailAsync(email);
            var result = await _userManager.AddToRoleAsync(employee, role);

            return result;
        }
    }
}
