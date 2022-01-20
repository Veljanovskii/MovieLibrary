using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MovieLibrary.Data.DataModels;
using MovieLibrary.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieLibrary.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly SignInManager<Employee> _signInManager;
        private readonly UserManager<Employee> _userManager;

        public LoginController(SignInManager<Employee> signInManager, UserManager<Employee> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        // POST api/<LoginController>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] LoginData loginData)
        {
            try
            {
                //var user = await _userManager.FindByEmailAsync(loginData.Email);
                //var password = await _userManager.CheckPasswordAsync(user, loginData.Password);

                var result = await _signInManager.PasswordSignInAsync(loginData.Email, loginData.Password, true, true);

                if (result.Succeeded)
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
