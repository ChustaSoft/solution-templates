using ProjectTemplate.Application.Users;
using ProjectTemplate.Dto.Users;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using UsersReadService = ProjectTemplate.Query.Application.Users;

namespace ProjectTemplate.WebApi.Controllers
{
    /// <summary>
    /// Contains endpoints for Registering, Editing and getting users.
    /// The Get-function calls an application service in the read stack.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly UsersReadService.IUserService userReadService;

        public UserController(IUserService userService, UsersReadService.IUserService userReadService)
        {
            this.userService = userService;
            this.userReadService = userReadService;
        }

        [HttpPost("RegisterUser")]
        public async Task RegisterUser([FromBody] RegisterUserDto dto)
        {
            await userService.RegisterUser(dto);
        }

        [HttpPost("EditUser")]
        public async Task EditUser([FromBody] EditUserDto dto)
        {
            await userService.EditUser(dto);
        }        

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var users = await userReadService.GetUserProfile(id);

            return Ok(users);
        }
    }
}
