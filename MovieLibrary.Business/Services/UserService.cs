using Microsoft.EntityFrameworkCore;
using MovieLibrary.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLibrary.Business
{
    public class UserService : IUserService
    {
        private readonly MovielibraryContext _db;

        public UserService(MovielibraryContext db)
        {
            _db = db;
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

            await _db.Users.AddAsync(user);
            await _db.SaveChangesAsync();
        }

        public async Task<List<UserDto>> GetAllUsers()
        {
            var userList = await _db.Users.Where(s => s.DeleteDate == null).Select(item => new UserDto
            {
                UserId = item.UserId,
                FirstName = item.FirstName,
                LastName = item.LastName,
                Address = item.Address,
                Idnumber = item.Idnumber,
                MaritalStatus = item.MaritalStatus.Caption,
                InsertDate = item.InsertDate,
                DeleteDate = item.DeleteDate

            }).ToListAsync();

            return userList;
        }

        public async Task<User> GetUser(int id)
        {
            return await _db.Users.Where(s => s.DeleteDate == null && s.UserId == id).FirstAsync();
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
            var targetUser = await _db.Users.FindAsync(id);

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
