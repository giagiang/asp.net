using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WebApplication.WebApi.Services;
using WebApplication.WebApi.ViewModels.Common;
using WebApplication.WebApi.ViewModels.Topics;

namespace WebApplication.WebApi.Controllers
{
    public class TopicsController : BaseController
    {
        private readonly ITopicService _topicService;

        public TopicsController(ITopicService topicService)
        {
            _topicService = topicService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Index([FromQuery] PagedAndSortedResultRequestDto requestDto)
        {
            return Ok(await _topicService.GetListAsync(requestDto));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm] TopicCreateDto dto)
        {
            return Ok(await _topicService.CreateAsync(dto));
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteAsync(Guid Id)
        {
            return Ok(await _topicService.DeleteAsync(Id));
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(Guid Id)
        {
            return Ok(await _topicService.GetById(Id));
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllListAsync([FromQuery] PagedAndSortedResultRequestDto requestDto)
        {
            return Ok(await _topicService.GetAllListAsync(requestDto));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] TopicUpdateDto dto)
        {
            return Ok(await _topicService.UpdateAsync(dto));
        }
    }
}