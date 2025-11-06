using AppSpeakers.Api.Controllers;
using AppSpeakers.Domain;
using Microsoft.Extensions.Logging;
using Moq;
using Speakers.Api.Services;
using Speakers.UnitTests;

namespace AppSpeakers.UnitTests
{
    public class Speaker_Controller
    {
        private Mock<ISpeakerService> _speakerService;
        private ILogger<SpeakersController> _logger = Mock.Of<ILogger<SpeakersController>>();

        public Speaker_Controller()
        {
            _speakerService = new Mock<ISpeakerService>();
        }


        [Fact]
        public void SpeakerController_Get()
        {
            //Arrange 
            var controller = new SpeakersController(_speakerService.Object, _logger);
            _speakerService.Setup(x => x.Get()).Returns(DataService.GetSpeakers());

            //Act
            var actionResult = controller.Get();

            //Assert
            Assert.NotNull(actionResult);
            Assert.Equal(2, actionResult.Count);
        }

        [Fact]
        public async Task SpeakerController_GetById()
        {
            var _id = "0a0ba06d-4135-b0c0-b223-63af9d798236";
            //Arrange 
            var controller = new SpeakersController(_speakerService.Object, _logger);
            _speakerService.Setup(x => x.GetById(It.IsAny<string>())).Returns(Task.FromResult(DataService.GetSpeaker()));

            //Act
            var actionResult = await controller.GetById(_id);

            //Assert
            Assert.NotNull(actionResult);
            Assert.Equal(_id, actionResult.Id);
        }

        [Fact]
        public async Task SpeakerController_Create()
        {
            //Arrange
            var _id = "8d5b57fe-8e17-4152-be99-7266958d0f8e";
            var speaker = new Speaker()
            {
                Id = _id,
                Name = "new_test",
                WebSite = "new_website",
                Bio = "unit_test"
            };
            var controller = new SpeakersController(_speakerService.Object, _logger);
            _speakerService.Setup(x => x.AddSpeaker(It.IsAny<Speaker>())).Returns(Task.FromResult(speaker));

            //Act
            var actionResult = await controller.AddSpeaker(speaker);

            //Assert
            Assert.NotNull(actionResult);
        }

        [Fact]
        public async Task SpeakerController_Delete()
        {
            //Arrange
            var _id = "8d5b57fe-8e17-4152-be99-7266958d0f8e";
            var speaker = new Speaker()
            {
                Id = _id,
                Name = "new_test",
                WebSite = "new_website",
                Bio = "unit_test"
            };
            var controller = new SpeakersController(_speakerService.Object, _logger);
            _speakerService.Setup(x => x.DeleteSpeaker(It.IsAny<string>())).Returns(Task.FromResult(speaker));

            //Act
            var actionResult = await controller.DeleteSpeaker(_id);

            //Assert
            Assert.NotNull(actionResult);
        }

    }
}
