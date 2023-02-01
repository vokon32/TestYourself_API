using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TestYourself_API.Dto;
using TestYourself_API.Models;

namespace TestYourself_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signinManager)
        {
            _userManager = userManager;
            _signInManager = signinManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            var response = new LoginDto();
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid) return View(loginDto);

            var user = await _userManager.FindByEmailAsync(loginDto.EmailAddress);
            if (user != null)
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginDto.Password);
                if (passwordCheck)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginDto.Password, false, false);
                    if (result.Succeeded)
                    {
                        return Ok();
                    }
                }
                return BadRequest("Wrong crednetials. Please, try again");
            }
            return BadRequest("Wrong crednetials. Please, try again");
        }
    }
}
