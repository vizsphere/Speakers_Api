using AppSpeakers.Domain;
using Moq;
using Speakers.Api.Repositories;
using Speakers.Api.Services;

namespace Speakers.UnitTests
{
    public class Speaker_Service
    {
        private Mock<ISpeakerRepository> _repository;

        public Speaker_Service()
        {
            _repository = new Mock<ISpeakerRepository>();
        }

        [Fact]
        public void SpeakerService_Get()
        {
            //Arrange 
            var service = new SpeakerService(_repository.Object);
            _repository.Setup(x => x.Get()).Returns(DataService.GetSpeakers());

            //Act
            var result = service.Get();

            //Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task SpeakerService_GetById()
        {
            var _id = "0a0ba06d-4135-b0c0-b223-63af9d798236";

            //Arrange 
            var service = new SpeakerService(_repository.Object);
            _repository.Setup(x => x.GetByIdAsync(It.IsAny<string>())).Returns(Task.FromResult(DataService.GetSpeaker()));

            //Act
            var result = await service.GetById(_id);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(_id, result.Id);
        }

        [Fact]
        public async Task SpeakerService_Add()
        {
            var _id = "8d5b57fe-8e17-4152-be99-7266958d0f8e";
            var speaker = new Speaker()
            {
                Id = _id,
                Name = "new_test",
                WebSite = "new_website",
                Bio = "unit_test"
            };

            //Arrange 
            var service = new SpeakerService(_repository.Object);
            _repository.Setup(x => x.Create(It.IsAny<Speaker>())).Returns(Task.FromResult(speaker));

            //Act
            var result = await service.AddSpeaker(speaker);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task SpeakerService_Delete()
        {
            var _id = "8d5b57fe-8e17-4152-be99-7266958d0f8e";
            var speaker = new Speaker()
            {
                Id = _id,
                Name = "new_test",
                WebSite = "new_website",
                Bio = "unit_test"
            };

            //Arrange 
            var service = new SpeakerService(_repository.Object);
            _repository.Setup(x => x.Delete(It.IsAny<string>())).Returns(Task.FromResult(speaker));

            //Act
            var result = await service.DeleteSpeaker(_id);

            //Assert
            Assert.NotNull(result);
        }
    }
}
