using Microsoft.EntityFrameworkCore;
using AppSpeakers.Domain;

namespace AppSpeakers.Api.Models
{
    public class SpeakerDbContext : DbContext
    {
        public SpeakerDbContext(DbContextOptions<SpeakerDbContext> options) : base(options)
        {
                
        }


        public SpeakerDbContext() { }


        public DbSet<Speaker> Speaker { get; set; }
    }
}
