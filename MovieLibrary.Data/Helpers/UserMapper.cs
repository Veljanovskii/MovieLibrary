using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieLibrary.Data.DataModels;
using MovieLibrary.Data.Models;

namespace MovieLibrary.Data.Helpers
{
    public class UserMapper
    {
        private readonly MovielibraryContext _db;

        public UserMapper(MovielibraryContext db)
        {
            _db = db;
        }

        public UserDto MapUserToDto(User user)
        {
            UserDto userDto = new UserDto
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Address = user.Address,
                Idnumber = user.Idnumber,
                MaritalStatus = _db.MaritalStatuses.Where(s => s.MaritalStatusId == user.MaritalStatusId).Select(s => s.Caption).First(),
                InsertDate = user.InsertDate,
                DeleteDate = user.DeleteDate
            };

            return userDto;
        }

        public User MapDtoToUser(UserDto userDto)
        {
            User user = new User
            {
                UserId = userDto.UserId,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Address = userDto.Address,
                Idnumber = userDto.Idnumber,
                MaritalStatusId = _db.MaritalStatuses.Where(s => s.Caption == userDto.MaritalStatus).Select(s => s.MaritalStatusId).First(),
                InsertDate = userDto.InsertDate,
                DeleteDate = userDto.DeleteDate
            };

            return user;
        }
    }
}
