using MovieLibrary.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLibrary.Business
{
    public interface IUserService
    {
        public Task InsertUser(UserCaption userCaption);
        public Task<List<UserCaption>> GetAllUsers();
        public Task<User> GetUser(int id);
        public Task<bool> EditUser(User user);
        //public Task<bool> DeleteUser(int id);
    }
}
