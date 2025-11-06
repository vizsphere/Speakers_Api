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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get()
        {
            _logger.LogInformation("Getting all speakers");
            var res =  _speakerService.Get();
            _logger.LogInformation("Retrieved {count} speakers", res.Count);

            return res == null || !res.Any() ? NotFound() : Ok(res);
        }

        [HttpGet]
        [Route("getSpeakerById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(string id)
        {
            _logger.LogInformation("Getting speaker by ID: {id}", id);
            var res= await _speakerService.GetById(id);
            _logger.LogInformation("Retrieved speaker: {speakerName}", res?.Name);

            return res == null ? NotFound() : Ok(res);
        }

        [HttpPost]
        [Route("searchSpeaker")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult SearchSpeaker(string searchTerm)
        {
            _logger.LogInformation("Searching speaker with term: {searchTerm}", searchTerm);
            var res = _speakerService.Search(searchTerm);
            _logger.LogInformation("Found {count} speakers matching search term", res.Count());

            return res == null ? NotFound() : Ok(res);
        }

        [HttpPost]
        [Route("addSpeaker")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddSpeaker(Speaker speaker)
        {
            _logger.LogInformation("Adding new speaker: {speakerName}", speaker.Name);
            Speaker res = new();
            try
            {
                 res = await _speakerService.AddSpeaker(speaker);
                _logger.LogInformation("Added speaker with ID: {speakerId}", res.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding speaker: {errorMessage}", ex.Message);
            }
            return res == null ? BadRequest() : Ok(res);
        }

        [HttpPost]
        [Route("updateSpeaker")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateSpeaker(Speaker speaker)
        {
            _logger.LogInformation("Updating speaker with ID: {speakerId}", speaker.Id);
            Speaker res = new();
            try
            {
                res = await _speakerService.UpdateSpeaker(speaker);
                _logger.LogInformation("Updated speaker: {speakerName}", res.Name);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating speaker: {errorMessage}", ex.Message); 
            }
            return res == null ? BadRequest() : Ok(res);
        }

        [HttpPost]
        [Route("deleteSpeaker")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteSpeaker(string id)
        {
            Speaker res = new();
            try
            {
                _logger.LogInformation("Deleting speaker with ID: {speakerId}", id);
                res = await _speakerService.DeleteSpeaker(id);
                _logger.LogInformation("Deleted speaker: {speakerName}", res?.Name);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting speaker: {errorMessage}", ex.Message);
            }
            return res == null ? BadRequest() : Ok(res);
        }
    }
}
