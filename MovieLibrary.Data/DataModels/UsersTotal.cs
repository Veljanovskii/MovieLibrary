using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLibrary.Data.DataModels
{
    public class UsersTotal
    {
        public List<UserDto> Users { get; set; }
        public int TotalUsers { get; set; }
    }
}
