using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieLibrary.Data.DataModels;
using MovieLibrary.Data.Models;

namespace MovieLibrary.Business
{
    public class MovieService : IMovieService
    {
        private readonly MovielibraryContext _db;

        public MovieService(MovielibraryContext db)
        {
            _db = db;
        }

        public async Task InsertMovie(Movie movie)
        {
            Movie newMovie = new Movie
            {
                Caption = movie.Caption,
                InsertDate = movie.InsertDate,
                ReleaseYear = movie.ReleaseYear,
                MovieLength = movie.MovieLength
            };

            await _db.Movies.AddAsync(movie);
            await _db.SaveChangesAsync();
        }

        public async Task<MoviesTotal> GetMovies(string sort, string order, int page)
        {
            IQueryable<Movie> moviesQuery;

            if (order == "" || order == "asc")
            {
                moviesQuery = _db.Movies.Where(s => s.DeleteDate == null).OrderBy(s => s.InsertDate).Skip(page * 30).Take(30);
            }
            else
            {
                moviesQuery = _db.Movies.Where(s => s.DeleteDate == null).OrderByDescending(s => s.InsertDate).Skip(page * 30).Take(30);
            }

            MoviesTotal moviesTotal = new MoviesTotal
            {
                Movies = await moviesQuery.ToListAsync(),
                TotalMovies = await _db.Movies.CountAsync()
            };

            return moviesTotal;
        }
        

        public async Task<Movie> GetMovie(int id)
        {
            return await _db.Movies.Where(s => s.DeleteDate == null && s.MovieId == id).FirstAsync();
        }

        public async Task<bool> EditMovie(Movie movie)
        {
            var targetMovie = await GetMovie(movie.MovieId);

            if (targetMovie != null)
            {
                targetMovie.Caption = movie.Caption;
                targetMovie.ReleaseYear = movie.ReleaseYear;
                targetMovie.MovieLength = movie.MovieLength;

                await _db.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> DeleteMovie(int id)
        {
            var targetMovie = await _db.Movies.FindAsync(id);

            if (targetMovie != null)
            {
                targetMovie.DeleteDate = DateTime.Now;

                await _db.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
