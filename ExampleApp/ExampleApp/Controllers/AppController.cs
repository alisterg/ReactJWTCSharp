using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ExampleApp.Exceptions;
using ExampleApp.Models;
using ExampleApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExampleApp.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AppController : Controller
    {
        private IRemarkService _remarkService;
        private IAuthService _authService;

        public AppController(IRemarkService remarkService, IAuthService authService)
        {
            _remarkService = remarkService;
            _authService = authService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Remark>>> GetAllRemarks()
        {
            // return all remarks
            
            var list = await _remarkService.GetAll();

            if (list == null)
            {
                return NotFound();
            }

            return list;
        }

        [HttpPost]
        public IActionResult AddRemark([FromBody] Remark remark)
        {
            string auth = Request.Headers["Authorization"];

            if (string.IsNullOrEmpty(auth))
            {
                return BadRequest();
            }
            
            // get the JWT token from the header
            string token = auth.Replace("Bearer ", "");
            string username;

            try
            {
                // extract the username from jwt, save remark against that user...
                username = _authService.UserFromToken(token);
            }
            catch (InvalidUserException)
            {
                return Unauthorized();
            }

            remark.Username = username;
            
            try
            {
                _remarkService.Create(remark);
            }
            catch (Exception)
            {
                // The db failed
                return BadRequest();
            }

            return Created("AddRemark", remark);
        }
    }
}