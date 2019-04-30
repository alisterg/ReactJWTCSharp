using System;
using LandmarkRemarks.Exceptions;
using LandmarkRemarks.Models;
using LandmarkRemarks.Services;
using Microsoft.AspNetCore.Mvc;

namespace LandmarkRemarks.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : Controller
    {
        private IUserService _userService;
        private IAuthService _authService;

        public AuthController(IUserService userService, IAuthService authService)
        {
            _userService = userService;
            _authService = authService;
        }
        
        [HttpPost]
        public IActionResult Login([FromBody] User loginUser)
        {
            // get the user and check if password is valid
            var userFromRepo = _userService.GetByUsername(loginUser.Username);
            bool isValidLogin = false;

            if (userFromRepo != null)
            {
                isValidLogin = _authService.CompareHash(loginUser.Password, userFromRepo.Password);
            }
            
            if (!isValidLogin)
            {
                return Unauthorized();
            }

            // generate the JWT token for use with requests
            string token = _authService.GenerateUserToken(loginUser.Username);

            return Json(new
            {
                token = token
            });
        }
    }
}