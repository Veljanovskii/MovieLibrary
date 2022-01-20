using Microsoft.EntityFrameworkCore;
using MovieLibrary.Data.DataModels;
using MovieLibrary.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieLibrary.Data.Helpers;

namespace MovieLibrary.Business
{
    public class UserService : IUserService
    {
        private readonly MovielibraryContext _db;
        private readonly UserMapper _mapper;

        public UserService(MovielibraryContext db)
        {
            _db = db;
            _mapper = new UserMapper(_db);
        }

        public async Task InsertUser(UserDto userDto)
        {
            User user = new User()
            {
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Address = userDto.Address,
                Idnumber = userDto.Idnumber,
                InsertDate = DateTime.Now
            };

            var maritalStatusId = await _db.MaritalStatuses.Where(s => s.Caption == userDto.MaritalStatus).Select(s => s.MaritalStatusId).FirstAsync();

            user.MaritalStatusId = maritalStatusId;

            await _db.Customers.AddAsync(user);
            await _db.SaveChangesAsync();
        }

        public async Task<UsersTotal> GetUsers(string sort, string order, int page, int size, string search)
        {
            IQueryable<User> usersQuery = _db.Customers.OrderBy(s => s.UserId);

            switch (sort)
            {
                case "FirstName":
                    if (order == "desc")
                        usersQuery = _db.Customers.OrderByDescending(s => s.FirstName);
                    else
                        usersQuery = _db.Customers.OrderBy(s => s.FirstName);
                    break;
                case "LastName":
                    if (order == "desc")
                        usersQuery = _db.Customers.OrderByDescending(s => s.LastName);
                    else
                        usersQuery = _db.Customers.OrderBy(s => s.LastName);
                    break;
                case "Address":
                    if (order == "desc")
                        usersQuery = _db.Customers.OrderByDescending(s => s.Address);
                    else
                        usersQuery = _db.Customers.OrderBy(s => s.Address);
                    break;
                case "Idnumber":
                    if (order == "desc")
                        usersQuery = _db.Customers.OrderByDescending(s => s.Idnumber);
                    else
                        usersQuery = _db.Customers.OrderBy(s => s.Idnumber);
                    break;
                case "MaritalStatus":
                    if (order == "desc")
                        usersQuery = _db.Customers.OrderByDescending(s => s.MaritalStatus);
                    else
                        usersQuery = _db.Customers.OrderBy(s => s.MaritalStatus);
                    break;
                case "InsertDate":
                    if (order == "desc")
                        usersQuery = _db.Customers.OrderByDescending(s => s.InsertDate);
                    else
                        usersQuery = _db.Customers.OrderBy(s => s.InsertDate);
                    break;
            }

            List<User> users;
            int total;

            if (search != null && search.Length > 2)
            {
                users = await usersQuery
                    .Where(s => s.DeleteDate == null)
                    .Where(s => s.FirstName.Contains(search) || s.LastName.Contains(search))
                    .Skip(page * size)
                    .Take(size)
                    .ToListAsync();
                total = await usersQuery
                    .Where(s => s.DeleteDate == null)
                    .Where(s => s.FirstName.Contains(search) || s.LastName.Contains(search))
                    .CountAsync();
            }
            else
            {
                users = await usersQuery.Where(s => s.DeleteDate == null)
                    .Skip(page * size)
                    .Take(size)
                    .ToListAsync();
                total = await usersQuery.Where(s => s.DeleteDate == null).CountAsync();
            }

            List<UserDto> dtoUsers = new List<UserDto>();

            foreach (var user in users)
            {
                dtoUsers.Add(_mapper.MapUserToDto(user));
            }

            UsersTotal usersTotal = new UsersTotal
            {
                Users = dtoUsers,
                TotalUsers = total
            };

            return usersTotal;
        }

        public async Task<User> GetUser(int id)
        {
            return await _db.Customers.Where(s => s.DeleteDate == null && s.UserId == id).FirstAsync();
        }

        public async Task<bool> EditUser(UserDto user)
        {
            var targetUser = await GetUser(user.UserId);

            if (targetUser != null)
            {
                targetUser.FirstName = user.FirstName;
                targetUser.LastName = user.LastName;
                targetUser.Address = user.Address;
                targetUser.Idnumber = user.Idnumber;

                var maritalStatusId = await _db.MaritalStatuses.Where(s => s.Caption == user.MaritalStatus).Select(s => s.MaritalStatusId).FirstAsync();
                targetUser.MaritalStatusId = maritalStatusId;

                await _db.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> DeleteUser(int id)
        {
            var targetUser = await _db.Customers.FindAsync(id);

            if (targetUser != null)
            {
                targetUser.DeleteDate = DateTime.Now;

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
