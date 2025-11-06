using AppSpeakers.Api.Models;
using AppSpeakers.Domain;

namespace Speakers.Api.Repositories
{
    public interface ISpeakerRepository : IGenericRepository<Speaker>
    {

    }

    public class SpeakerRepository : GenericRepository<Speaker>, ISpeakerRepository
    {
        private readonly SpeakerDbContext _dbContext;

        public SpeakerRepository(SpeakerDbContext dbContext) : base(dbContext) 
        {
            _dbContext = dbContext; 
        }
    }
}
