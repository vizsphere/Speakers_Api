using AppSpeakers.Domain;
using Microsoft.AspNetCore.Mvc;
using Speakers.Api.Services;

namespace AppSpeakers.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SpeakersController : ControllerBase
    {
        private readonly ILogger<SpeakersController> _logger;
        private readonly ISpeakerService _speakerService;

        public SpeakersController(ISpeakerService speakerService, ILogger<SpeakersController> logger)
        {
            _logger = logger;
            _speakerService = speakerService;
        }

        [HttpGet]
        [Route("getSpeakers")]
        public IList<Speaker> Get()
        {
            return _speakerService.Get();
        }

        [HttpGet]
        [Route("getSpeakerById")]
        public async Task<Speaker> GetById(string id)
        {
            return await _speakerService.GetById(id);
        }

        [HttpPost]
        [Route("searchSpeaker")]
        public IEnumerable<Speaker> SearchSpeaker(string searchTerm)
        {
            _logger.LogInformation("Searching speaker with term: {searchTerm}", searchTerm);

            return _speakerService.Search(searchTerm);
        }

        [HttpPost]
        [Route("addSpeaker")]
        public async Task<Speaker> AddSpeaker(Speaker speaker)
        {
            return await _speakerService.AddSpeaker(speaker);
        }

        [HttpPost]
        [Route("updateSpeaker")]
        public async Task<Speaker> UpdateSpeaker(Speaker speaker)
        {
            return await _speakerService.UpdateSpeaker(speaker);
        }

        [HttpPost]
        [Route("deleteSpeaker")]
        public async Task<Speaker> DeleteSpeaker(string id)
        {
            return await _speakerService.DeleteSpeaker(id);
        }

    }
}
