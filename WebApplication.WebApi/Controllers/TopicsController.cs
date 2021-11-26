using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.WebApi.Services;
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
        public async Task<IActionResult> Index()
        {
            return Ok(await _topicService.GetListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(TopicCreateDto dto)
        {
            return Ok(await _topicService.CreateAsync(dto));
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            return Ok(await _topicService.DeleteAsync(id));
        }
    }
}