using Microsoft.AspNetCore.Mvc;
using TestYourself_API.Dto;
using TestYourself_API.Models;
using TestYourself_API.Repository;

namespace TestYourself_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        [HttpGet("users")]
        [ProducesResponseType(200, Type = typeof(AppUser))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Index()
        {
            var users = await _userRepository.GetAllUsers();
            List<UserDto> result = new();
            foreach (var user in users)
            {
                var userViewModel = new UserDto()
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    TestPassed = user.TestResults.Count,
                    ImageUrl = user.ProfileImageUrl
                };
                result.Add(userViewModel);
            }
            return Ok(result);
        }

        [HttpGet("detail/{userId}")]
        [ProducesResponseType(200, Type = typeof(AppUser))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Detail(string userId)
        {
            var user = await _userRepository.GetUserById(userId);
            var userDetailViewModel = new UserDto()
            {
                Id = user.Id,
                UserName = user.UserName
            };
            return Ok(userDetailViewModel);
        }
    }
}
