﻿using MovieLibrary.Data.DataModels;
using MovieLibrary.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLibrary.Business.Services
{
    public interface IRentMovieService
    {
        public Task<List<Movie>> GetMovies(string search, string idNumber);
        public Task<bool> RentMovies(RentRequest rentReqest);
        public Task<bool> ReturnMovies(RentRequest returnReqest);
        public Task<bool> CheckValid(string idNumber);
        public Task<List<Movie>> GetRentedForUser(string idNumber);
    }
}
