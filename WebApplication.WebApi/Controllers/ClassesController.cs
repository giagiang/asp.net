using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WebApplication.WebApi.Services;
using WebApplication.WebApi.ViewModels.Classes;
using WebApplication.WebApi.ViewModels.Common;

namespace WebApplication.WebApi.Controllers
{
    public class ClassesController : BaseController
    {
        private readonly IClassService _classService;

        public ClassesController(IClassService classService)
        {
            _classService = classService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetListAsync([FromQuery] PagedAndSortedResultRequestDto requestDto)
        {
            return Ok(await _classService.GetListAsync(requestDto));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateClassDto dto)
        {
            return Ok(await _classService.CreateAsync(dto));
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteAsync(Guid Id)
        {
            return Ok(await _classService.DeleteAsync(Id));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateClassDto dto)
        {
            return Ok(await _classService.UpdateAsync(dto));
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(Guid Id)
        {
            return Ok(await _classService.GetById(Id));
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllListAsync([FromQuery] PagedAndSortedResultRequestDto requestDto)
        {
            return Ok(await _classService.GetAllListAsync(requestDto));
        }
    }
}