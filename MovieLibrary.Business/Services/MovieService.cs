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
                MovieLength = movie.MovieLength,
                Quantity = movie.Quantity,
                Avatar = movie.Avatar
            };

            await _db.Movies.AddAsync(newMovie);
            await _db.SaveChangesAsync();
        }

        public async Task<MoviesTotal> GetMovies(string sort, string order, int page, int size, string search)
        {
            IQueryable<Movie> moviesQuery = _db.Movies.OrderBy(s => s.MovieId);

            switch (sort)
            {
                case "Caption":
                    if (order == "desc")
                        moviesQuery = _db.Movies.OrderByDescending(s => s.Caption);
                    else
                        moviesQuery = _db.Movies.OrderBy(s => s.Caption);
                    break;
                case "Release":
                    if (order == "desc")
                        moviesQuery = _db.Movies.OrderByDescending(s => s.ReleaseYear);
                    else
                        moviesQuery = _db.Movies.OrderBy(s => s.ReleaseYear);
                    break;
                case "Length":
                    if (order == "desc")
                        moviesQuery = _db.Movies.OrderByDescending(s => s.MovieLength);
                    else
                        moviesQuery = _db.Movies.OrderBy(s => s.MovieLength);
                    break;
                case "Insert":
                    if (order == "desc")
                        moviesQuery = _db.Movies.OrderByDescending(s => s.InsertDate);
                    else
                        moviesQuery = _db.Movies.OrderBy(s => s.InsertDate);
                    break;
            }

            List<Movie> movies;
            int total;

            if (search != null && search.Length > 2)
            {
                movies = await moviesQuery
                    .Where(s => s.DeleteDate == null)
                    .Where(s => s.Caption.Contains(search))
                    .Skip(page * size)
                    .Take(size)
                    .ToListAsync();
                total = await moviesQuery
                    .Where(s => s.DeleteDate == null)
                    .Where(s => s.Caption.Contains(search))
                    .CountAsync();
            }
            else 
            {
                movies = await moviesQuery.Where(s => s.DeleteDate == null)
                    .Skip(page * size)
                    .Take(size)
                    .ToListAsync();
                total = await moviesQuery.Where(s => s.DeleteDate == null).CountAsync();
            }

            MoviesTotal moviesTotal = new MoviesTotal
            {
                Movies = movies,
                TotalMovies = total
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
                targetMovie.Quantity = movie.Quantity;
                targetMovie.Avatar = movie.Avatar;

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
