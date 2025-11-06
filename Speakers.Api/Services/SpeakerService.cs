using AppSpeakers.Domain;
using Speakers.Api.Repositories;

namespace Speakers.Api.Services
{
    public interface ISpeakerService
    {
        IList<Speaker> Get();

        Task<Speaker> GetById(string id);

        Task<Speaker> AddSpeaker(Speaker speaker);

        Task<Speaker> UpdateSpeaker(Speaker speaker);

        Task<Speaker> DeleteSpeaker(string id);

        IEnumerable<Speaker> Search(string term);
    }

    public class SpeakerService : ISpeakerService
    {
        private readonly ISpeakerRepository _speakerRepository;

        public SpeakerService(ISpeakerRepository speakerRepository)
        {
            _speakerRepository = speakerRepository; 
        }

        public IList<Speaker> Get()
        {
            return _speakerRepository.Get();
        }

        public async Task<Speaker> GetById(string id)
        {
            return await _speakerRepository.GetByIdAsync(id);
        }

        public async Task<Speaker> AddSpeaker(Speaker speaker)
        {
            return await _speakerRepository.Create(speaker); 
        }
        public async Task<Speaker> UpdateSpeaker(Speaker speaker)
        {
            return await _speakerRepository.Update(speaker);
        }

        public async Task<Speaker> DeleteSpeaker(string id)
        {
            return await _speakerRepository.Delete(id);
        }

        public IEnumerable<Speaker> Search(string term)
        {
            var test =  _speakerRepository.Search(
                p => p.Bio.ToLower().Contains(term.ToLower()));

            return test;
        }
    }
}
