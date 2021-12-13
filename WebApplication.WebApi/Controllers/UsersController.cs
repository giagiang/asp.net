using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;
using WebApplication.WebApi.Services;
using WebApplication.WebApi.SignalR;
using WebApplication.WebApi.ViewModels.Common;
using WebApplication.WebApi.ViewModels.Users;

namespace WebApplication.WebApi.Controllers
{
    public class UsersController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IHubContext<SignalHub> _hubContext;

        public UsersController(IUserService userService, IHubContext<SignalHub> hubContext)
        {
            _hubContext = hubContext;
            _userService = userService;
        }

        [HttpPost("Authenticate")]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate([FromBody] UserLoginRequest request)
        {
            var result = await _userService.Authenticate(request);
            await _hubContext.Clients.All.SendAsync("online");
            if (result.IsSuccessed) return Ok(result);
            return BadRequest(result.Message);
        }

        [HttpGet]
        public async Task<IActionResult> GetListAsync([FromQuery] PagedAndSortedResultRequestDto request)
        {
            return Ok(await _userService.GetListAsync(request));
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> CreateAsync([FromBody] CreateUserDto request)
        {
            var result = await _userService.CreateAsync(request);
            if (result == null) return BadRequest(result.Message);
            return Ok(result.IsSuccessed);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromForm] UpdateUserDto request, Guid Id)
        {
            var result = await _userService.UpdateAsync(Id, request);
            if (result != null) return Ok(result);
            return BadRequest();
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteAsync(Guid Id)
        {
            var result = await _userService.DeleteAsync(Id);
            if (result == true) return Ok(result);
            return BadRequest(result);
        }

        [HttpPost("addpresmission")]
        public async Task<IActionResult> AddPermission(Guid Id, string claimValue)
        {
            var kq = await _userService.PermissionUser(Id, claimValue);
            if (kq == true) return Ok(kq);
            return BadRequest(false);
        }

        [HttpPut("roles")]
        public async Task<IActionResult> RoleAssign([FromBody] RoleAssignRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userService.RoleAssign(request.Id, request);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var user = await _userService.GetById(id);
            return Ok(user);
        }

        [HttpGet("GetListRole")]
        public async Task<IActionResult> GetListRole()
        {
            return Ok(await _userService.GetListRole());
        }

        [HttpPost("CourseAssign")]
        public async Task<IActionResult> CourseAssign(CourseAssignRequest request)
        {
            return Ok(await _userService.CourseAssign(request));
        }

        [HttpPost("ClassAssign")]
        public async Task<IActionResult> ClassAssign(ClassAssignRequest request)
        {
            return Ok(await _userService.ClassAssign(request));
        }

        [HttpGet("GetCourseByUserId")]
        public async Task<IActionResult> GetCourseByUserId(Guid Id)
        {
            return Ok(await _userService.GetCourseByIdUser(Id));
        }
        [HttpGet("GetClassByUserId")]
        public async Task<IActionResult> GetClassByUserId(Guid Id)
        {
            return Ok(await _userService.GetCLassByIdUser(Id));
        }
    }
}