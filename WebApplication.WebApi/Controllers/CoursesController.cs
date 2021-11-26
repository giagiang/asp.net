using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.WebApi.Services;
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
        public async Task<IActionResult> GetListAsync()
        {
            return Ok(await _courseService.GetListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateCourseDto dto)
        {
            return Ok(await _courseService.CreateAsync(dto));
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteAsync(Guid Id)
        {
            return Ok(await _courseService.DeleteAsync(Id));
        }
    }
}