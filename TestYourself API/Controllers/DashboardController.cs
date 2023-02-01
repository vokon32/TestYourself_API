using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Mvc;
using TestYourself_API.Dto;
using TestYourself_API.Helper;
using TestYourself_API.Models;
using TestYourself_API.Repository;

namespace TestYourself_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : Controller
    {
        private readonly IDashboardRepository _dashboardRepository;
        private readonly IPhotoService _photoService;

        public DashboardController(IDashboardRepository dashboardRepository,
            IPhotoService photoService)
        {
            _dashboardRepository = dashboardRepository;
            _photoService = photoService;
        }
        private static void MapUserEdit(AppUser user, EditUserDashboardDto editVM, ImageUploadResult photoResult)
        {
            user.Id = editVM.Id;
            user.ProfileImageUrl = photoResult.Url.ToString();
            user.City = editVM.City;
            user.State = editVM.State;
        }

        [HttpGet("index")]
        [ProducesResponseType(200, Type = typeof(Test))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Index()
        {
            var userTests = await _dashboardRepository.GetAllUserTests();
            var dashboardVM = new DashboardDto()
            {
                Tests = userTests
            };
            return Ok(dashboardVM);
        }
        [HttpGet("edituserprofile")]
        [ProducesResponseType(200, Type = typeof(Test))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> EditUserProfile()
        {
            var curUserId = User.GetUserId();
            var user = await _dashboardRepository.GetUserById(curUserId);
            if (user == null) return View("Error");
            var editUserVM = new EditUserDashboardDto()
            {
                Id = curUserId,
                ProfileImageUrl = user.ProfileImageUrl,
                City = user.City,
                State = user.State
            };
            return Ok(editUserVM);
        }

        [HttpPost("edituserprofile")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> EditUserProfile([FromForm]EditUserDashboardDto editVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit profile");
                return Ok("EditUserProfile");
            }

            var user = _dashboardRepository.GetUserByIdNoTracking(editVM.Id).Result;

            if (user.ProfileImageUrl == "" || user.ProfileImageUrl == null)
            {
                var photoResult = await _photoService.AddPhotoAsync(editVM.Image);

                MapUserEdit(user, editVM, photoResult);

                _dashboardRepository.Update(user);
                return RedirectToAction("Index");
            }
            else
            {
                try
                {
                    await _photoService.DeletePhotoAsync(user.ProfileImageUrl);
                }
                catch
                {
                    ModelState.AddModelError("", "Could not delete photo");
                    return Ok(editVM);
                }
               
                var photoResult = await _photoService.AddPhotoAsync(editVM.Image);
                MapUserEdit(user, editVM, photoResult);
                _dashboardRepository.Update(user);
                return RedirectToAction("Index");
            }
        }
    }
}
