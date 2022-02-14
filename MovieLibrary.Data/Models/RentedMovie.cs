using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLibrary.Data.Models
{
    public class RentedMovie
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public int UserId { get; set; }
        public DateTime RentDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        public User User { get; set; }
        public Movie Movie { get; set; }
    }
}
