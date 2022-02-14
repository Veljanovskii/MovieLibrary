using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieLibrary.Business.Services;
using MovieLibrary.Data.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieLibrary.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentMovieController : ControllerBase
    {

        private readonly IRentMovieService _rentMovieService;

        public RentMovieController(IRentMovieService rentMovieService)
        {
            _rentMovieService = rentMovieService;
        }


        [HttpGet("Search")]
        public async Task<IActionResult> Search(string search, string idNumber)
        {
            try
            {
                var list = await _rentMovieService.GetMovies(search, idNumber);

                if (list != null)
                    return Ok(list);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetRented")]
        public async Task<IActionResult> GetRentedMovies(string idNumber)
        {
            try
            {
                var list = await _rentMovieService.GetRentedForUser(idNumber);

                if (list != null)
                    return Ok(list);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("CheckValid")]
        public async Task<IActionResult> CheckValid(string idNumber)
        {
            try
            {
                var found = await _rentMovieService.CheckValid(idNumber);

                return Ok(found);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> RentMovies([FromBody] RentRequest rentRequest)
        {
            try
            {
                var success = await _rentMovieService.RentMovies(rentRequest);

                return Ok(success);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("Return")]
        public async Task<IActionResult> ReturnMovies([FromBody] RentRequest rentRequest)
        {
            try
            {
                var success = await _rentMovieService.ReturnMovies(rentRequest);

                return Ok(success);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
