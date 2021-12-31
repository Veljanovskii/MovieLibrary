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

            await _db.Movies.AddAsync(newMovie);
            await _db.SaveChangesAsync();
        }

        public async Task<MoviesTotal> GetMovies(string sort, string order, int page, int size)
        {
            IQueryable<Movie> moviesQuery = _db.Movies.OrderBy(s => s.MovieId).Skip(page * size).Take(size);

            switch (sort)
            {
                case "Caption":
                    if (order == "desc")
                        moviesQuery = _db.Movies.OrderByDescending(s => s.Caption).Skip(page * size).Take(size);
                    else
                        moviesQuery = _db.Movies.OrderBy(s => s.Caption).Skip(page * size).Take(size);
                    break;
                case "Release year":
                    if (order == "desc")
                        moviesQuery = _db.Movies.OrderByDescending(s => s.ReleaseYear).Skip(page * size).Take(size);
                    else
                        moviesQuery = _db.Movies.OrderBy(s => s.ReleaseYear).Skip(page * size).Take(size);
                    break;
                case "Length":
                    if (order == "desc")
                        moviesQuery = _db.Movies.OrderByDescending(s => s.MovieLength).Skip(page * size).Take(size);
                    else
                        moviesQuery = _db.Movies.OrderBy(s => s.MovieLength).Skip(page * size).Take(size);
                    break;
                case "Insert date":
                    if (order == "desc")
                        moviesQuery = _db.Movies.OrderByDescending(s => s.InsertDate).Skip(page * size).Take(size);
                    else
                        moviesQuery = _db.Movies.OrderBy(s => s.InsertDate).Skip(page * size).Take(size);
                    break;
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
