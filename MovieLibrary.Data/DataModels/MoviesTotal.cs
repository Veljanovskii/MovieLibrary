using MovieLibrary.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLibrary.Data.DataModels
{
    public class MoviesTotal
    {
        public List<Movie> Movies { get; set; }
        public int TotalMovies { get; set; }
    }
}
