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
        private static MovielibraryContext db = new MovielibraryContext();

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

            var maritalStatusId = await db.MaritalStatuses.Where(s => s.Caption == userDto.MaritalStatus).Select(s => s.MaritalStatusId).FirstAsync();

            user.MaritalStatusId = maritalStatusId;

            await db.Users.AddAsync(user);
            await db.SaveChangesAsync();
        }

        public async Task<List<UserDto>> GetAllUsers()
        {
            var userList = await db.Users.Where(s => s.DeleteDate == null).Select(item => new UserDto
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
            return await db.Users.Where(s => s.DeleteDate == null && s.UserId == id).FirstAsync();
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

                var maritalStatusId = await db.MaritalStatuses.Where(s => s.Caption == user.MaritalStatus).Select(s => s.MaritalStatusId).FirstAsync();
                targetUser.MaritalStatusId = maritalStatusId;

                await db.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> DeleteUser(int id)
        {
            var targetUser = await db.Users.FindAsync(id);

            if (targetUser != null)
            {
                targetUser.DeleteDate = DateTime.Now;
                await db.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
