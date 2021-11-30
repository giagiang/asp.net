using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WebApplication.WebApi.Services;
using WebApplication.WebApi.ViewModels.Common;
using WebApplication.WebApi.ViewModels.Courses;

namespace WebApplication.WebApi.Controllers
{
    public class CoursesController : BaseController
    {
        private readonly ICourseService _courseService;

        public CoursesController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpGet]
        public async Task<IActionResult> GetListAsync([FromQuery] PagedAndSortedResultRequestDto requestDto)
        {
            return Ok(await _courseService.GetListAsync(requestDto));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm] CreateCourseDto dto)
        {
            return Ok(await _courseService.CreateAsync(dto));
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteAsync(Guid Id)
        {
            return Ok(await _courseService.DeleteAsync(Id));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateCourseDto dto)
        {
            return Ok(await _courseService.UpdateAsync(dto));
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(Guid Id)
        {
            return Ok(await _courseService.GetById(Id));
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllListAsync([FromQuery] PagedAndSortedResultRequestDto requestDto)
        {
            return Ok(await _courseService.GetAllListAsync(requestDto));
        }
    }
}