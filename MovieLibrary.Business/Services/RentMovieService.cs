using Microsoft.EntityFrameworkCore;
using MovieLibrary.Data.DataModels;
using MovieLibrary.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLibrary.Business.Services
{
    public class RentMovieService : IRentMovieService
    {
        private readonly MovielibraryContext _db;

        public RentMovieService(MovielibraryContext db)
        {
            _db = db;
        }

        public async Task<bool> CheckValid(string idNumber)
        {
            var customer = await _db.Customers.Where(c => c.Idnumber == idNumber).FirstOrDefaultAsync();

            if (customer != null)
                return true;
            else
                return false;
        }

        public async Task<List<MovieLight>> GetMovies(string search, string idNumber)
        {
            return await _db.Movies
                .Where(s => s.DeleteDate == null
                && s.Caption.Contains(search)
                && s.Quantity > _db.RentedMovies
                    .Where(r => r.MovieId == s.MovieId
                        && r.ReturnDate == null)
                    .Count()
                && _db.Movies.Select(m => m.MovieId).Except(_db.RentedMovies
                    .Where(r => r.User.Idnumber == idNumber
                        && r.ReturnDate == null)
                    .Select(r => r.MovieId)
                    )
                    .Contains(s.MovieId)
                ).Select(s => new MovieLight { MovieId = s.MovieId, Caption = s.Caption })
                .ToListAsync();
        }

        public async Task<List<MovieLight>> GetShowMovies(List<int> movies)
        {
            List<MovieLight> result = new List<MovieLight>();

            foreach (var movie in movies)
            {
                var movieLight = await _db.Movies
                    .Where(s => s.MovieId == movie)
                    .Select(s => new MovieLight { MovieId = s.MovieId, Caption = s.Caption })
                    .FirstOrDefaultAsync();

                result.Add(movieLight);
            }

            return result;
        }

        public async Task<List<Movie>> GetRentedForUser(string idNumber)
        {
            return await _db.Movies
                .Where(s => s.DeleteDate == null && _db.RentedMovies
                        .Where(r => r.User.Idnumber == idNumber && r.ReturnDate == null)
                        .Select(r => r.MovieId)
                        .ToList()
                        .Contains(s.MovieId)                )
                .ToListAsync();
        }

        public async Task<bool> RentMovies(RentRequest rentReqest)
        {
            var targetUser = await _db.Customers.Where(c => c.DeleteDate == null && c.Idnumber == rentReqest.SelectedIDnumber).FirstAsync();
            if (targetUser == null)
            {
                return false;
            }

            List<RentedMovie> rentedMovies = new List<RentedMovie>();

            foreach(var item in rentReqest.Movies)
            {
                var targetMovie = await _db.Movies.Where(m => m.DeleteDate == null && m.MovieId == item).FirstAsync();

                rentedMovies.Add(new RentedMovie()
                {
                    MovieId = item,
                    Movie = targetMovie,
                    UserId = targetUser.UserId,
                    User = targetUser,
                    RentDate = DateTime.UtcNow,
                    ReturnDate = null
                });
            }

            await _db.RentedMovies.AddRangeAsync(rentedMovies);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ReturnMovies(RentRequest returnReqest)
        {
            var targetUser = await _db.Customers.Where(c => c.DeleteDate == null && c.Idnumber == returnReqest.SelectedIDnumber).FirstAsync();
            if (targetUser == null)
            {
                return false;
            }

            foreach (var item in returnReqest.Movies)
            {
                var target = await _db.RentedMovies
                    .Where(r => r.User.Idnumber == returnReqest.SelectedIDnumber
                        && r.MovieId == item
                        && r.ReturnDate == null)
                    .FirstOrDefaultAsync();
                target.ReturnDate = DateTime.UtcNow;
                await _db.SaveChangesAsync();
            }

            return true;
        }
    }
}
