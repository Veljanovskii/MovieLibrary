using MovieLibrary.Data.DataModels;
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
        public Task InsertMovie(Movie movie);
        public Task<MoviesTotal> GetMovies(string sort, string order, int page, int size, string search);
        public Task<Movie> GetMovie(int id);
        public Task<bool> EditMovie(Movie movie);
        public Task<bool> DeleteMovie(int id);
    }
}
