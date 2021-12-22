using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieLibrary.Business;
using MovieLibrary.Data.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MovieLibrary.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public UserService userService;

        public UserController()
        {
            userService = new UserService();
        }

        // GET: api/<UserController>All
        [HttpGet("All")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var list = await userService.GetAllUsers();

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

        // POST api/<UserController>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] UserCaption user)
        {
            try
            {
                await userService.InsertUser(user);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var user = await userService.GetUser(id);

                if (user != null)
                    return Ok(user);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //// PUT api/<UserController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<UserController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
