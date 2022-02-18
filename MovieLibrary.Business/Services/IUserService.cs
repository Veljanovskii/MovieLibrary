using MovieLibrary.Data.DataModels;
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
        public Task InsertUser(UserDto user);
        public Task<UsersTotal> GetUsers(string sort, string order, int page, int size, string search);
        public Task<UserDto> GetUser(int id);
        public Task<bool> EditUser(UserDto user);
        public Task<bool> DeleteUser(int id);
    }
}
