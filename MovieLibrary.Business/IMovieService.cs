using MovieLibrary.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLibrary.Business
{
    public interface IMovieService
    {
        public Task InsertMovie(string caption, int releaseYear, DateTime insertDate);
        public Task<List<Movie>> GetAllMovies();
        public Task<Movie> GetMovie(int id);
        public Task<bool> EditMovie(Movie movie);
        public Task<bool> DeleteMovie(int id);
    }
}
