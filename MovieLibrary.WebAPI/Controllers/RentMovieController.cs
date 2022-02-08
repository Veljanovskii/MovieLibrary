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

        // GET: api/<MovieController>
        [HttpGet]
        public async Task<IActionResult> Get(string search)
        {
            try
            {
                var list = await _rentMovieService.GetMovies(search);

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
        public async Task<IActionResult> Add([FromBody] RentRequest rentRequest)
        {
            try
            {
                await _rentMovieService.RentMovies(rentRequest);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
