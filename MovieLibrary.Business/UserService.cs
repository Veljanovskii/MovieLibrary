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

        public async Task InsertUser(UserCaption userCaption)
        {
            User user = new User()
            {
                FirstName = userCaption.FirstName,
                LastName = userCaption.LastName,
                Address = userCaption.Address,
                Idnumber = userCaption.Idnumber,
                InsertDate = DateTime.Now
            };

            var maritalStatusId = await db.MaritalStatuses.Where(s => s.Caption == userCaption.MaritalStatus).Select(s => s.MaritalStatusId).FirstAsync();

            user.MaritalStatusId = maritalStatusId;

            await db.Users.AddAsync(user);
            await db.SaveChangesAsync();
        }

        public async Task<List<UserCaption>> GetAllUsers()
        {
            var userList = await db.Users.Where(s => s.DeleteDate == null).Select(item => new UserCaption
            {
                //UserId = item.UserId,
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

        public async Task<bool> EditUser(User user) //usercaption
        {
            var targetUser = await GetUser(user.UserId);

            if (targetUser != null)
            {
                targetUser.FirstName = user.FirstName;
                targetUser.LastName = user.LastName;
                targetUser.Address = user.Address;
                targetUser.Idnumber = user.Idnumber;

                var maritalStatusId = 

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
