using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieLibrary.Data.Models;

namespace MovieLibrary.Business
{
    public class MovieService : IMovieService
    {
        private static MovielibraryContext db = new MovielibraryContext();

        public async Task InsertMovie(string caption, int releaseYear, DateTime insertDate)
        {
            Movie movie = new Movie();
            movie.Caption = caption;
            movie.InsertDate = insertDate;
            movie.ReleaseYear = releaseYear;

            await db.Movies.AddAsync(movie);
            await db.SaveChangesAsync();
        }

        public async Task<List<Movie>> GetAllMovies()
        {
            return await db.Movies.Where(s => s.DeleteDate == null).ToListAsync();
        }

        public async Task<Movie> GetMovie(int id)
        {
            return await db.Movies.Where(s => s.DeleteDate == null && s.MovieId == id).FirstAsync();
        }

        public async Task<bool> EditMovie(Movie movie)
        {
            var targetMovie = await GetMovie(movie.MovieId);

            if (targetMovie != null)
            {
                targetMovie.Caption = movie.Caption;
                targetMovie.ReleaseYear = movie.ReleaseYear;
                await db.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> DeleteMovie(int id)
        {
            var targetMovie = await db.Movies.FindAsync(id);

            if (targetMovie != null)
            {
                targetMovie.DeleteDate = DateTime.Now;
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
