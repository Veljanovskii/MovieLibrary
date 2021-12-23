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
        public Task InsertUser(UserDto userCaption);
        public Task<List<UserDto>> GetAllUsers();
        public Task<User> GetUser(int id);
        public Task<bool> EditUser(UserDto user);
        //public Task<bool> DeleteUser(int id);
    }
}
