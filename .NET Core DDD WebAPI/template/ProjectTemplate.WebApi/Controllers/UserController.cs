using $ext_safeprojectname$.Application.Users;
using $ext_safeprojectname$.Dto.Users;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using UsersReadService = $ext_safeprojectname$.Query.Application.Users;

namespace $ext_safeprojectname$.WebApi.Controllers
{
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
