using ActiveDirectory_API.Helpers;
using ActiveDirectory_API.Models;
using ActiveDirectory_API.Repositories.IRepositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System.DirectoryServices.Protocols;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ActiveDirectory_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IUserService _user;

        public UsersController(IConfiguration config, IUserService user, IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
            _user = user;
        }

        [Route("AuthenticateUser")]
        [HttpPost]
        //[ApiVersion("1.0")]
        //[Route("v{version:apiVersion}/AuthenticateUser")]
        public async Task<IActionResult> AuthenticateUser(UserCredential user)
        {
            try
            {
                var data = await _user.AuthenticateUser(user);

                return Ok(new APIResponse
                {
                    status_code = (int)HttpStatusCode.OK,
                    Message = "Success",
                    data = data,
                    //Count = data.ToArray().Length
                });
            }
            catch (Exception ex)
            {

                return BadRequest(new APIResponse
                {
                    status_code = (int)HttpStatusCode.BadRequest,
                    Message = ex.Message.ToString(),
                    Count = 0
                });
            }
        }

        [Route("GetAllUsersData")]
        [HttpGet]
        public async Task<IActionResult> GetAllUsersData()
        {
            try
            {
                var data = await _user.GetAllUsersData();

                return Ok(new APIResponse
                {
                    status_code = (int)HttpStatusCode.OK,
                    Message = "Success",
                    data = data,
                    //Count = data.ToArray().Length
                });
            }
            catch (Exception ex)
            {

                return BadRequest(new APIResponse
                {
                    status_code = (int)HttpStatusCode.BadRequest,
                    Message = ex.Message.ToString(),
                    Count = 0
                });
            }
        }

        [Route("GetUserData")]
        [HttpGet]
        public async Task<IActionResult> GetUserData(string username)
        {
            try
            {
                var data = await _user.GetUserData(username);

                return Ok(new APIResponse
                {
                    status_code = (int)HttpStatusCode.OK,
                    Message = "Success",
                    data = data,
                    //Count = data.ToArray().Length
                });
            }
            catch (Exception ex)
            {

                return BadRequest(new APIResponse
                {
                    status_code = (int)HttpStatusCode.BadRequest,
                    Message = ex.Message.ToString(),
                    Count = 0
                });
            }
        }

        [Route("GetUserDataFormatted")]
        [HttpGet]
        public async Task<IActionResult> GetUserDataFormatted()
        {
            try
            {
                var data = await _user.GetUserDataFormatted();

                return Ok(new APIResponse
                {
                    status_code = (int)HttpStatusCode.OK,
                    Message = "Success",
                    data = data,
                    //Count = data.ToArray().Length
                });
            }
            catch (Exception ex)
            {

                return BadRequest(new APIResponse
                {
                    status_code = (int)HttpStatusCode.BadRequest,
                    Message = ex.Message.ToString(),
                    Count = 0
                });
            }
        }
    }
}
