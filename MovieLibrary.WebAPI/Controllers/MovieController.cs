using Microsoft.AspNetCore.Mvc;
using MovieLibrary.Business;
using MovieLibrary.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MovieLibrary.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        // GET: api/<MovieController>All
        [HttpGet("All")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var list = await _movieService.GetAllMovies();

                if (list != null)
                    return Ok(list);
                else
                    return NotFound();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/<MovieController>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Movie movie)
        {
            try
            {
                await _movieService.InsertMovie(movie);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/<MovieController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var movie = await _movieService.GetMovie(id);

                if (movie != null)
                    return Ok(movie);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        // PUT api/<MovieController>/5
        [HttpPut("Edit")]
        public async Task<IActionResult> Edit([FromBody] Movie movie)
        {
            try
            {
                var found = await _movieService.EditMovie(movie);

                if (found)
                    return Ok();
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<MovieController>/5
        [HttpPut("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var found = await _movieService.DeleteMovie(id);

                if (found)
                    return Ok();
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
