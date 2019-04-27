using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NoSQL.Entities;
using NoSQL.Models.DTOs;
using NoSQL.Services.Authentication.JwtTokenSource;

namespace NoSQL.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly IJwtTokenSource tokenSource;

        public AccountController(UserManager<User> userManager, 
            SignInManager<User> signInManager,
            IJwtTokenSource tokenSource)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.tokenSource = tokenSource;
        }

        [HttpPost]
        public async Task<IActionResult> Register ([FromBody]RegisterDTO dto)
        {
            var user = new User
            {
                Email = dto.Email,
                UserName = dto.Email,
                Name = dto.Name         
            };
            var registration = await userManager.CreateAsync(user, dto.Password);

            if (!registration.Succeeded)
                throw new Exception();

            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<string>> Login([FromBody]LoginDTO dto)
        {
            var result = await signInManager.PasswordSignInAsync(dto.Email, dto.Password, false, false);

            if (!result.Succeeded)
                throw new Exception();

            return Ok(tokenSource.Token);
        }
    }
}
