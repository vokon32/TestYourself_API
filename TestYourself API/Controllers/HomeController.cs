using Microsoft.AspNetCore.Mvc;
using TestYourself_API.Models;
using TestYourself_API.Repository;

namespace TestYourself_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : Controller
    {
        private readonly IHomeRepository _homeRepository;

        public HomeController(IHomeRepository homeRepository)
        {
            _homeRepository = homeRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Test>))]
        public async Task<IActionResult> Index()
        {
            var homeDto = await _homeRepository.GetHomeDto();
            return Ok(homeDto);
        }
        [HttpGet("privacy")]
        public IActionResult Privacy()
        {
            return Ok();
        }
    }
}
